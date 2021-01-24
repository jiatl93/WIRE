using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/// <summary>
///     Attribute for use with Newtonsoft JSON to encrypt/decrypt
///     string values on the fly as they are serialized/deserialized.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonEncryptAttribute : Attribute
{
}

/// <summary>
///     Resolver used by JSON configuration object to supply en/decryption
///     password. Password can be any length, since it hashes the value
///     to the correct number of bytes for the AES algorithm.
/// </summary>
public class EncryptedStringPropertyResolver : DefaultContractResolver
{
    /// <summary>
    ///     The encryption key bytes
    /// </summary>
    private readonly byte[] encryptionKeyBytes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EncryptedStringPropertyResolver" /> class.
    /// </summary>
    /// <param name="encryptionKey">The encryption key.</param>
    /// <exception cref="ArgumentNullException">encryptionKey</exception>
    public EncryptedStringPropertyResolver(string encryptionKey)
    {
        if (encryptionKey == null)
            throw new ArgumentNullException("encryptionKey");

        // Hash the key to ensure it is exactly 256 bits long, as required by AES-256
        using (var sha = new SHA256Managed())
        {
            encryptionKeyBytes =
                sha.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
        }
    }

    /// <summary>
    ///     Creates properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.
    /// </summary>
    /// <param name="type">The type to create properties for.</param>
    /// <param name="memberSerialization">The member serialization mode for the type.</param>
    /// <returns>
    ///     Properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.
    /// </returns>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var props = base.CreateProperties(type, memberSerialization);

        // Find all string properties that have a [JsonEncrypt] attribute applied
        // and attach an EncryptedStringValueProvider instance to them
        foreach (var prop in props.Where(p => p.PropertyType == typeof(string)))
        {
            var pi = type.GetProperty(prop.UnderlyingName);
            if (pi != null && pi.GetCustomAttribute(typeof(JsonEncryptAttribute), true) != null)
                prop.ValueProvider =
                    new EncryptedStringValueProvider(pi, encryptionKeyBytes);
        }

        return props;
    }

    /// <summary>
    ///     Main provider for en/decryption attribute.
    /// </summary>
    private class EncryptedStringValueProvider : IValueProvider
    {
        private readonly byte[] encryptionKey;
        private readonly PropertyInfo targetProperty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncryptedStringValueProvider" /> class.
        /// </summary>
        /// <param name="targetProperty">The target property.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        public EncryptedStringValueProvider(PropertyInfo targetProperty, byte[] encryptionKey)
        {
            this.targetProperty = targetProperty;
            this.encryptionKey = encryptionKey;
        }

        /// <summary>
        ///     GetValue is called by Json.Net during serialization.
        ///     The target parameter has the object from which to read the unencrypted string;
        ///     the return value is an encrypted string that gets written to the JSON
        /// </summary>
        /// <param name="target">The target to get the value from.</param>
        /// <returns>
        ///     The value.
        /// </returns>
        public object GetValue(object target)
        {
            var value = (string) targetProperty.GetValue(target);
            var buffer = Encoding.UTF8.GetBytes(value);

            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged {Key = encryptionKey})
            {
                var iv = aes.IV; // first access generates a new IV
                outputStream.Write(iv, 0, iv.Length);
                outputStream.Flush();

                var encryptor = aes.CreateEncryptor(encryptionKey, iv);
                using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                {
                    inputStream.CopyTo(cryptoStream);
                }

                return Convert.ToBase64String(outputStream.ToArray());
            }
        }

        /// <summary>
        ///     SetValue gets called by Json.Net during deserialization.
        ///     The value parameter has the encrypted value read from the JSON;
        ///     target is the object on which to set the decrypted value.
        /// </summary>
        /// <param name="target">The target to set the value on.</param>
        /// <param name="value">The value to set on the target.</param>
        /// <exception cref="CryptographicException">IV is missing or invalid.</exception>
        public void SetValue(object target, object value)
        {
            var buffer = Convert.FromBase64String((string) value);

            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged {Key = encryptionKey})
            {
                var iv = new byte[16];
                var bytesRead = inputStream.Read(iv, 0, 16);
                if (bytesRead < 16) throw new CryptographicException("IV is missing or invalid.");

                var decryptor = aes.CreateDecryptor(encryptionKey, iv);
                using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputStream);
                }

                var decryptedValue = Encoding.UTF8.GetString(outputStream.ToArray());
                targetProperty.SetValue(target, decryptedValue);
            }
        }
    }
}
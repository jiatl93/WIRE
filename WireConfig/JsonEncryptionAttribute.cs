// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="JsonEncryptAttribute.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WireCommon;

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
    ///     Initializes a new instance of the <see cref="EncryptedStringPropertyResolver" /> class.
    /// </summary>
    /// <param name="encryptionKey">The encryption key.</param>
    /// <exception cref="ArgumentNullException">encryptionKey</exception>
    public EncryptedStringPropertyResolver(string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptionKey))
            throw new ArgumentNullException("encryptionKey");

        AesProvider.EncryptionKey = encryptionKey;
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
                    new EncryptedStringValueProvider(pi);
        }

        return props;
    }

    /// <summary>
    ///     Main provider for en/decryption attribute.
    /// </summary>
    private class EncryptedStringValueProvider : IValueProvider
    {
        private readonly PropertyInfo targetProperty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncryptedStringValueProvider" /> class.
        /// </summary>
        /// <param name="targetProperty">The target property.</param>
        public EncryptedStringValueProvider(PropertyInfo targetProperty)
        {
            this.targetProperty = targetProperty;
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
            var plainText = (string) targetProperty.GetValue(target);
            return AesProvider.Encrypt(plainText);
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
            var cryptText = (string) value;
            targetProperty.SetValue(target, AesProvider.Decrypt(cryptText));
        }
    }
}
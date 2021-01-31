// ***********************************************************************
// Assembly         : WireCli
// Author           : jiatli93
// Created          : 01-15-2021
//
// Last Modified By : jiatli93
// Last Modified On : 01-31-2021
// ***********************************************************************
// <copyright file="AesProvider.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WireCommon
{
    /// <summary>
    ///     Wrapper for AES symmetric cryptography.
    /// </summary>
    public class AesProvider
    {
        private const string IV_ERROR = "IV is missing or invalid";

        private const string KEY_ERROR = "Encryption Key not set";

        /// <summary>
        ///     The encryption key bytes
        /// </summary>
        private static byte[] _keyBytes;

        /// <summary>
        ///     The encryption key
        /// </summary>
        private static string _encryptionKey;

        /// <summary>
        ///     Gets or sets the encryption key.
        /// </summary>
        /// <value>
        ///     The encryption key.
        /// </value>
        public static string EncryptionKey
        {
            get => _encryptionKey;

            set
            {
                _encryptionKey = value;
                using (var sha = new SHA256Managed())
                {
                    _keyBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(_encryptionKey));
                }
            }
        }

        /// <summary>
        ///     Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>Encrypted text</returns>
        /// <exception cref="System.Security.Cryptography.CryptographicException">Encryption Key not set.</exception>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(_encryptionKey))
                throw new CryptographicException(KEY_ERROR);

            var buffer = Encoding.UTF8.GetBytes(plainText);

            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged {Key = _keyBytes})
            {
                var iv = aes.IV; // first access generates a new IV
                outputStream.Write(iv, 0, iv.Length);
                outputStream.Flush();

                var encryptor = aes.CreateEncryptor(_keyBytes, iv);
                using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                {
                    inputStream.CopyTo(cryptoStream);
                }

                return Convert.ToBase64String(outputStream.ToArray());
            }
        }

        /// <summary>
        ///     Decrypts the specified crypt text.
        /// </summary>
        /// <param name="cryptText">The crypt text.</param>
        /// <returns>Decrypted text</returns>
        /// <exception cref="System.Security.Cryptography.CryptographicException">IV is missing or invalid.</exception>
        public static string Decrypt(string cryptText)
        {
            if (string.IsNullOrEmpty(_encryptionKey))
                throw new CryptographicException(KEY_ERROR);

            var buffer = Convert.FromBase64String(cryptText);

            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged {Key = _keyBytes})
            {
                var iv = new byte[16];
                var bytesRead = inputStream.Read(iv, 0, 16);
                if (bytesRead < 16) throw new CryptographicException(IV_ERROR);

                var decryptor = aes.CreateDecryptor(_keyBytes, iv);
                using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputStream);
                }

                return Encoding.UTF8.GetString(outputStream.ToArray());
            }
        }
    }
}
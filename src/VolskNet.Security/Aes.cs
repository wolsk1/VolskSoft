namespace VolskSoft.Bibliotheca.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class AES
    {
        /// <summary>
        /// Encrypts the string to bytes.
        /// </summary>
        /// <param name="dataString">The data string.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>Encrypted data</returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static byte[] EncryptStringToBytes(string dataString, string key, string iv)
        {
            if (dataString == null || dataString.Length <= 0)
                throw new ArgumentNullException(nameof(dataString));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            byte[] encrypted;

            using (var algorythm = new RijndaelManaged())
            {
                algorythm.Key = Encoding.UTF8.GetBytes(key);
                algorythm.IV = Encoding.UTF8.GetBytes(iv);

                var encryptor = algorythm.CreateEncryptor(algorythm.Key, algorythm.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(dataString);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypts the string from bytes.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static string DecryptStringFromBytes(byte[] cipherText, string key, string iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            string plaintext;

            using (var algorythm = new RijndaelManaged())
            {
                algorythm.Key = Encoding.UTF8.GetBytes(key);
                algorythm.IV = Encoding.UTF8.GetBytes(iv);

                var decryptor = algorythm.CreateDecryptor(algorythm.Key, algorythm.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
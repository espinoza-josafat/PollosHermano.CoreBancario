using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PollosHermano.MicroFramework.Tools.Security
{
    public class CryptSecurity : ICryptSecurity
    {
        readonly string _appKey;
        readonly string _appIv;

        public CryptSecurity(string appKey, string appIv)
        {
            _appKey = appKey ?? throw new ArgumentNullException("appKey");
            _appIv = appIv ?? throw new ArgumentNullException("appIv");
        }
        
        /// <summary>
        /// Encrypt or decrypt ciperText.
        /// </summary>
        /// <param name="cipherText">string: text</param>
        /// <param name="mode">EncryptionMode.Encrypt or EncryptionMode.Decrypt</param>
        /// <returns>string: based on mode used.</returns>
        public string EncryptDecrypt(string cipherText, EncryptionMode mode)
        {
            string result = string.Empty;
            
            var keybytes = Encoding.UTF8.GetBytes(_appKey);
            var iv = Encoding.UTF8.GetBytes(_appIv);

            if (mode == EncryptionMode.Encrypt)
            {
                var response = EncryptStringToBytes(cipherText, keybytes, iv);
                result = response == null ? string.Empty : Convert.ToBase64String(response);
            }
            if (mode == EncryptionMode.Decrypt)
            {
                var encrypted = Convert.FromBase64String(cipherText);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                result = decriptedFromJavascript;
            }

            return result;
        }

        /// <summary>
        /// Use the mehtod to decrypt the encrypted value.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>string</returns>
        string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                        // Read the decrypted bytes from the decrypting stream  
                        // and place them in a string.  
                        plaintext = srDecrypt.ReadToEnd();
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        /// <summary>
        /// Use this method to encrypt string.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>return: string in encrypted form.</returns>
        byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted = null;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for encryption.  
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.  
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }
    }
}

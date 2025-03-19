using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;



namespace EncriptacinDistribuidos.Algorithms
{
    public class MyAES : IAlgorthm
    {
        public string GetName()
        {
            return "AES";
        }

        public void ToEncrypt(string data, int year)
        {
            try
            {
                // Configuración de la clave y el IV para el cifrado AES
                string key = "1234567890123456";  // Clave de 128 bits (16 bytes)
                string iv = "1234567890123456";   // Vector de inicialización (IV)

                byte[] encryptedData = EncryptStringToBytes_Aes(data, key, iv);
                string decryptedData = DecryptStringFromBytes_Aes(encryptedData, key, iv);

                Console.WriteLine("Texto descifrado: {0}", decryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el cifrado AES: " + ex.Message);
            }
        }

        // Método para cifrar datos usando AES
        private static byte[] EncryptStringToBytes_Aes(string plainText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        // Método para descifrar datos usando AES
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
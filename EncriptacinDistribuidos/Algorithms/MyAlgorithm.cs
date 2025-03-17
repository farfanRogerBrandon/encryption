using System;
using System.Text;
using EncriptacinDistribuidos;

namespace EncriptacionDistribuidos.Algorithms
{
    internal class MyAlgorithm
    {
        public string GetName()
        {
            return "MyAlgorithm";
        }

        public void ToEncrypt(string data, int birthYear)
        {
            try
            {
                Console.WriteLine("dsadasd" + data);
                string encryptedData = Encrypt(data, birthYear);
                Console.WriteLine("Encrypted: " + encryptedData);

                string decryptedData = Decrypt(encryptedData, birthYear);
                Console.WriteLine("Decrypted: " + decryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption failed: " + ex.Message);
            }
        }

        private string Encrypt(string data, int birthYear)
        {
            // Convertir la cadena original a Base64 para preservar caracteres chinos
            string base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));

            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in base64Data)
            {
                encryptedText.Append((char)(c - birthYear));
            }

            return encryptedText.ToString();
        }

        private string Decrypt(string encryptedData, int birthYear)
        {
            StringBuilder decryptedText = new StringBuilder();
            foreach (char c in encryptedData)
            {
                decryptedText.Append((char)(c + birthYear));
            }

            // Convertir de Base64 a texto original
            byte[] utf8Bytes = Convert.FromBase64String(decryptedText.ToString());
            return Encoding.UTF8.GetString(utf8Bytes);
        }
    }
}

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
            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in data)
            {
                encryptedText.Append((c - birthYear) + " ");
            }
            return encryptedText.ToString().Trim();
        }

        private string Decrypt(string encryptedData, int birthYear)
        {
            StringBuilder decryptedText = new StringBuilder();
            string[] numbers = encryptedData.Split(' ');

            foreach (string num in numbers)
            {
                if (int.TryParse(num, out int asciiValue))
                {
                    decryptedText.Append((char)(asciiValue + birthYear));
                }
            }
            return decryptedText.ToString();
        }
    }
}

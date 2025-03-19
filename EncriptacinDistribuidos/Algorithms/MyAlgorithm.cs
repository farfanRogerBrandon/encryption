using System;
using System.Text;
using EncriptacinDistribuidos;

namespace EncriptacionDistribuidos.Algorithms
{
    internal class MyAlgorithm : IAlgorthm
    {
        public string GetName()
        {
            return "MyAlgorithm";
        }

        public void ToEncrypt(string data, int birthYear)
        {
            try
            {
                // Permitir UTF-8 en la entrada y salida
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;

                string encryptedData = Encrypt(data, birthYear);

                string decryptedData = Decrypt(encryptedData, birthYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption failed: " + ex.Message);
            }
        }

        private string Encrypt(string data, int birthYear)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(data);
            StringBuilder encryptedText = new StringBuilder();

            foreach (byte b in utf8Bytes)
            {
                // Desplazamiento circular dentro del rango de bytes válidos
                encryptedText.Append((char)((b + birthYear) % 256));  // Asegura que el byte no se salga del rango
            }

            return encryptedText.ToString();
        }

        private string Decrypt(string encryptedData, int birthYear)
        {
            byte[] utf8Bytes = new byte[encryptedData.Length];

            for (int i = 0; i < encryptedData.Length; i++)
            {
                // Desplazamiento circular para la desencriptación
                utf8Bytes[i] = (byte)((encryptedData[i] - birthYear + 256) % 256);  // Asegura que el byte no se salga del rango
            }

            return Encoding.UTF8.GetString(utf8Bytes);
        }
    }
}

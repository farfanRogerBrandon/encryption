using System;
using System.Security.Cryptography;
using System.Text;

namespace EncriptacinDistribuidos.Algorithms
{
    internal class MySHA : IAlgorthm
    {
        public string GetName()
        {
            return "SHA-256";
        }

        public void ToEncrypt(string data)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())  // Crear instancia de SHA-256
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);  // Convertir la cadena en bytes
                    byte[] hashBytes = sha256.ComputeHash(dataBytes); // Generar el hash

                    string hashString = BitConverter.ToString(hashBytes).Replace("-", ""); // Convertir a cadena hexadecimal
                    Console.WriteLine($"SHA-256 Hash: {hashString}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SHA-256: {ex.Message}");
            }
        }
    }
}

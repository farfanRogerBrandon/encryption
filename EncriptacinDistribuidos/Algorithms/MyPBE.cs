using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Algorithms
{
    public class MyPBE: IAlgorthm
    {
        private readonly string password;
        private readonly byte[] salt = Encoding.UTF8.GetBytes("s@ltV@lu3"); // Un valor fijo para demostración

        public MyPBE()
        {
            this.password = "MiContraseñaSegura123";
        }

        public void ToEncrypt(string data)
        {
            try
            {
                byte[] encryptedData = EncryptData(data, password, out byte[] salt, out byte[] iv);
                Console.WriteLine($"Encrypted: {Convert.ToBase64String(encryptedData)}");

                string decripted = DecryptData(encryptedData, password, salt, iv);
                Console.WriteLine($"Decrypted: {Convert.ToBase64String(encryptedData)}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Encryption failed: {ex.Message}");
            }
        }

        public string GetName()
        {
            return "Password-Based Encryption (PBE) with AES";
        }

        static byte[] EncryptData(string plainText, string password, out byte[] salt, out byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateIV();
                iv = aes.IV;

                // Generar un salt aleatorio
                salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(salt);

                // Derivar clave con PBKDF2
                using (var keyDerivation = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                    aes.Key = keyDerivation.GetBytes(32);

                // Cifrar
                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                        writer.Write(plainText);
                    return ms.ToArray();
                }
            }
        }

        public string DecryptData(byte[] cipherText, string password, byte[] salt, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.IV = iv;

                // Derivar clave con PBKDF2
                using (var keyDerivation = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                    aes.Key = keyDerivation.GetBytes(32);

                // Descifrar
                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(cipherText))
                using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream))
                    return reader.ReadToEnd();
            }
        }
    }
}

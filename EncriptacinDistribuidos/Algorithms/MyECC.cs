using System;
using System.Security.Cryptography;
using System.Text;

namespace EncriptacinDistribuidos
{
    public class MyECC : IAlgorthm
    {
        private ECDiffieHellmanCng recipientKeyPair;
        private byte[] recipientPublicKey;
        private byte[] encryptedData;

        public MyECC()
        {
            // Inicializamos el par de claves al crear la instancia
            recipientKeyPair = new ECDiffieHellmanCng();
            recipientKeyPair.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            recipientKeyPair.HashAlgorithm = CngAlgorithm.Sha256;

            // Guardamos la clave pública
            recipientPublicKey = recipientKeyPair.PublicKey.ToByteArray();
        }

        public string GetName()
        {
            return "ECC (Elliptic Curve Cryptography)";
        }

        public void ToEncrypt(string data, int year)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            encryptedData = ECCEncrypt(dataToEncrypt, recipientPublicKey);
            Console.WriteLine("Texto encriptado (base64): " + Convert.ToBase64String(encryptedData));
        }

        public string ToDecrypt()
        {
            if (encryptedData == null)
            {
                return "No hay datos encriptados disponibles.";
            }

            try
            {
                byte[] decryptedData = ECCDecrypt(encryptedData);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException ex)
            {
                return $"Error al desencriptar: {ex.Message}";
            }
        }

        private byte[] ECCEncrypt(byte[] dataToEncrypt, byte[] recipientPublicKey)
        {
            using (ECDiffieHellmanCng sender = new ECDiffieHellmanCng())
            {
                sender.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                sender.HashAlgorithm = CngAlgorithm.Sha256;

                // Exportamos la clave pública del emisor para incluirla en el mensaje
                byte[] senderPublicKey = sender.PublicKey.ToByteArray();

                using (CngKey recipientKey = CngKey.Import(recipientPublicKey, CngKeyBlobFormat.EccPublicBlob))
                {
                    byte[] sharedSecret = sender.DeriveKeyMaterial(recipientKey);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = sharedSecret;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.GenerateIV();
                        byte[] iv = aes.IV;

                        using (var encryptor = aes.CreateEncryptor())
                        {
                            byte[] encryptedData = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

                            // Formato: [Tamaño de clave pública del emisor (4 bytes)][Clave pública del emisor][IV][Datos encriptados]
                            byte[] result = new byte[4 + senderPublicKey.Length + iv.Length + encryptedData.Length];

                            // Primero escribimos el tamaño de la clave pública
                            byte[] senderKeySize = BitConverter.GetBytes(senderPublicKey.Length);
                            Buffer.BlockCopy(senderKeySize, 0, result, 0, 4);

                            // Luego escribimos la clave pública del emisor
                            Buffer.BlockCopy(senderPublicKey, 0, result, 4, senderPublicKey.Length);

                            // Luego el IV
                            Buffer.BlockCopy(iv, 0, result, 4 + senderPublicKey.Length, iv.Length);

                            // Y finalmente los datos encriptados
                            Buffer.BlockCopy(encryptedData, 0, result, 4 + senderPublicKey.Length + iv.Length, encryptedData.Length);

                            return result;
                        }
                    }
                }
            }
        }

        private byte[] ECCDecrypt(byte[] encryptedData)
        {
            // Extraer la clave pública del emisor
            int senderKeySize = BitConverter.ToInt32(encryptedData, 0);
            byte[] senderPublicKey = new byte[senderKeySize];
            Buffer.BlockCopy(encryptedData, 4, senderPublicKey, 0, senderKeySize);

            // Importar la clave pública del emisor
            using (CngKey senderKey = CngKey.Import(senderPublicKey, CngKeyBlobFormat.EccPublicBlob))
            {
                // Obtener el secreto compartido usando la clave privada del destinatario
                byte[] sharedSecret = recipientKeyPair.DeriveKeyMaterial(senderKey);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = sharedSecret;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Extraer el IV
                    byte[] iv = new byte[16];
                    Buffer.BlockCopy(encryptedData, 4 + senderKeySize, iv, 0, iv.Length);
                    aes.IV = iv;

                    // Extraer los datos encriptados
                    int encryptedContentOffset = 4 + senderKeySize + iv.Length;
                    int encryptedContentSize = encryptedData.Length - encryptedContentOffset;
                    byte[] encryptedContent = new byte[encryptedContentSize];
                    Buffer.BlockCopy(encryptedData, encryptedContentOffset, encryptedContent, 0, encryptedContentSize);

                    // Desencriptar
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        return decryptor.TransformFinalBlock(encryptedContent, 0, encryptedContent.Length);
                    }
                }
            }
        }

        public void Dispose()
        {
            recipientKeyPair?.Dispose();
        }
    }
}
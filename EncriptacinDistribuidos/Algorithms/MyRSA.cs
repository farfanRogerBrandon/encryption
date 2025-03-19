using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Algorithms
{
    public class MyRSA : IAlgorthm
    {
        public string GetName()
        {
            return "RSA";
        }

        public void ToEncrypt(string data, int year)
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Convert the data string into a byte array
                byte[] dataToEncrypt = ByteConverter.GetBytes(data);
                byte[] encryptedData;
                byte[] decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))  // Use 2048 bits key
                {
                    //Pass the data to ENCRYPT, the public key information 
                    encryptedData = RSAEncryptInBlocks(dataToEncrypt, RSA.ExportParameters(false), false);

                    //Pass the data to DECRYPT, the private key information 
                    decryptedData = RSADecryptInBlocks(encryptedData, RSA.ExportParameters(true), false);

                    Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Encryption failed.");
            }
        }

        // Encrypt data in blocks based on RSA 2048 key
        public static byte[] RSAEncryptInBlocks(byte[] dataToEncrypt, RSAParameters RSAKeyInfo, bool doOaeppadding)
        {
            try
            {
                List<byte> encryptedDataList = new List<byte>();
                int blockSize = 245;  // 2048 bits key -> max 245 bytes per block (256 bytes - 11 bytes for padding)
                int totalBlocks = (int)Math.Ceiling((double)dataToEncrypt.Length / blockSize);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))  // 2048 bits key
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    for (int i = 0; i < totalBlocks; i++)
                    {
                        int currentBlockSize = (i == totalBlocks - 1) ? dataToEncrypt.Length - i * blockSize : blockSize;
                        byte[] blockToEncrypt = new byte[currentBlockSize];
                        Array.Copy(dataToEncrypt, i * blockSize, blockToEncrypt, 0, currentBlockSize);

                        byte[] encryptedBlock = RSA.Encrypt(blockToEncrypt, doOaeppadding);
                        encryptedDataList.AddRange(encryptedBlock);
                    }
                }

                return encryptedDataList.ToArray();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // Decrypt data in blocks based on RSA 2048 key
        public static byte[] RSADecryptInBlocks(byte[] dataToDecrypt, RSAParameters RSAKeyInfo, bool doOaeppadding)
        {
            try
            {
                List<byte> decryptedDataList = new List<byte>();
                int blockSize = 256;  // 2048 bits key -> 256 bytes per block (RSA decrypts in blocks of 256 bytes)
                int totalBlocks = (int)Math.Ceiling((double)dataToDecrypt.Length / blockSize);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))  // 2048 bits key
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    for (int i = 0; i < totalBlocks; i++)
                    {
                        int currentBlockSize = (i == totalBlocks - 1) ? dataToDecrypt.Length - i * blockSize : blockSize;
                        byte[] blockToDecrypt = new byte[currentBlockSize];
                        Array.Copy(dataToDecrypt, i * blockSize, blockToDecrypt, 0, currentBlockSize);

                        byte[] decryptedBlock = RSA.Decrypt(blockToDecrypt, doOaeppadding);
                        decryptedDataList.AddRange(decryptedBlock);
                    }
                }

                return decryptedDataList.ToArray();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        // Cifrado original (sin bloques)
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                Console.WriteLine(encryptedData.ToString());
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // Descifrado original (sin bloques)
        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

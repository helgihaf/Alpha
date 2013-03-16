using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Knightrunner.Library.Core;

namespace Knightrunner.Library.Cryptography
{
    /// <summary>
    /// Provides functions for encrypting and decrypting data with a secret key using symmetrical encryption algorithms.
    /// </summary>
    public class Symmetric
    {
        private const int InternalBufferSize = 4096;

        /// <summary>
        /// Creates a secret key that can be used to encrypt and decrypt data using symmetric
        /// algoritms.
        /// </summary>
        /// <returns>The secret key.</returns>
        public byte[] CreateSecretKey()
        {
            byte[] secretKey;
            using (var algorithm = CreateSymmetricAlgorithm())
            {
                secretKey = new byte[algorithm.Key.Length];
                Buffer.BlockCopy(algorithm.Key, 0, secretKey, 0, algorithm.Key.Length);
            }

            return secretKey;
        }


        /// <summary>
        /// Encrypts the specified data stream using the specified secret key. A symmetric algorithm
        /// is used to encrypt the data.
        /// </summary>
        /// <param name="dataStream">The data stream to encrypt.</param>
        /// <param name="secretKey">The secret key to use.</param>
        /// <param name="encryptedDataStream">The resulting encrypted data stream.</param>
        /// <exception cref="T:System.ArgumentNullException">dataStream, secretKey or encryptedDataStream are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be encrypted using the secretKey.</exception>
        public void EncryptData(Stream dataStream, byte[] secretKey, Stream encryptedDataStream)
        {
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");
            if (secretKey == null)
                throw new ArgumentNullException("secretKey");
            if (encryptedDataStream == null)
                throw new ArgumentNullException("encryptedDataStream");

            // Generate an IV
            using (SymmetricAlgorithm symmetricAlgorithm = CreateSymmetricAlgorithm())
            {
                symmetricAlgorithm.Key = secretKey;
                symmetricAlgorithm.GenerateIV();

                // Write IV
                encryptedDataStream.Write(symmetricAlgorithm.IV, 0, symmetricAlgorithm.IV.Length);

                // Encrypt the data
                EncryptSymmetric(symmetricAlgorithm, dataStream, encryptedDataStream);
            }
        }


        /// <summary>
        /// Encrypts the specified data using the specified secret key. A symmetric algorithm
        /// is used to encrypt the data.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="secretKey">The secret key to use.</param>
        /// <returns>The encrypted data.</returns>
        /// <exception cref="T:System.ArgumentNullException">data or secretKey are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be encrypted using the secretKey.</exception>
        public byte[] EncryptData(byte[] data, byte[] secretKey)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (secretKey == null)
                throw new ArgumentNullException("secretKey");

            using (MemoryStream resultStream = new MemoryStream(data.Length * 2))
            {
                using (MemoryStream dataStream = new MemoryStream(data, false))
                {
                    EncryptData(dataStream, secretKey, resultStream);
                }

                return resultStream.ToArray();
            }
        }


        /// <summary>
        /// Decrypts the specified encrypted data using the specified secret key. A symmetric
        /// algorithm is used to decrypt the data.
        /// </summary>
        /// <param name="encryptedDataStream">The encrypted data stream to be decrypted. The data in the stream
        /// must have been previously created by the EncryptData() function.</param>
        /// <param name="secretKey">The secretKey key to use.</param>
        /// <param name="dataStream">The data stream into which the decrypted data will be written.</param>
        /// <exception cref="T:System.ArgumentNullException">encryptedDataStream, secretKey or dataStream are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be decrypted using the secretKey.</exception>
        public void DecryptData(Stream encryptedDataStream, byte[] secretKey, Stream dataStream)
        {
            if (encryptedDataStream == null)
                throw new ArgumentNullException("encryptedDataStream");
            if (secretKey == null)
                throw new ArgumentNullException("secretKey");
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");

            // Create the symmetric encryption algorithm
            using (SymmetricAlgorithm symmetricAlgorithm = CreateSymmetricAlgorithm())
            {
                // Extract IV from the stream
                byte[] iv;
                using (BinaryReader reader = new BinaryReader(encryptedDataStream))
                {
                    // Read the IV
                    iv = reader.ReadBytes(symmetricAlgorithm.IV.Length);

                    // Initialize the symmetric algorithm with key and IV
                    symmetricAlgorithm.Key = secretKey;
                    symmetricAlgorithm.IV = iv;

                    DecryptSymmetric(symmetricAlgorithm, encryptedDataStream, dataStream);
                }
            }
        }



        /// <summary>
        /// Decrypts the specified encrypted data using the specified secret key. A symmetric
        /// algorithm is used to decrypt the data.
        /// </summary>
        /// <param name="encryptedData">The encrypted data to be decrypted. The data in the buffer
        /// must have been previously created by the EncryptData() function.</param>
        /// <param name="secretKey">The secretKey key to use.</param>
        /// <returns>The decrypted data.</returns>
        /// <exception cref="T:System.ArgumentNullException">encryptedData or secretKey are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be decrypted using the secretKey.</exception>
        public byte[] DecryptData(byte[] encryptedData, byte[] secretKey)
        {
            if (encryptedData == null)
                throw new ArgumentNullException("encryptedData");
            if (secretKey == null)
                throw new ArgumentNullException("secretKey");

            using (MemoryStream dataStream = new MemoryStream(encryptedData.Length))
            {
                using (MemoryStream encryptedDataStream = new MemoryStream(encryptedData))
                {
                    DecryptData(encryptedDataStream, secretKey, dataStream);
                }

                return dataStream.ToArray();
            }
        }


        /// <summary>
        /// Creates the symmetric cryptographic algorithm of choice used for encrypting large
        /// volume of data.
        /// </summary>
        protected virtual SymmetricAlgorithm CreateSymmetricAlgorithm()
        {
            // Create Rijndael algorithm using the default key size of 256
            return new RijndaelManaged();
        }




        /// <summary>
        /// Encrypts the specified data using the specified symmetric cryptographic algorithm and
        /// returns the encrypted data in a buffer.
        /// </summary>
        internal void EncryptSymmetric(SymmetricAlgorithm symmetricAlgorithm, Stream clearDataStream, Stream encryptedDataStream)
        {
            using (ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor())
            {
                using (CryptoStream csEncrypt = new CryptoStream(encryptedDataStream, transform, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[InternalBufferSize];
                    int length;
                    while ((length = clearDataStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        csEncrypt.Write(buffer, 0, length);
                    }
                    csEncrypt.FlushFinalBlock();
                }
            }
        }





        /// <summary>
        /// Decrypts the specified data using the specified symmetric cryptographic algorithm and
        /// returns the decrypted data in a buffer.
        /// </summary>
        internal void DecryptSymmetric(SymmetricAlgorithm symmetricAlgorithm, Stream encryptedDataStream, Stream dataStream)
        {
            using (ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor())
            {
                using (CryptoStream csDecrypt = new CryptoStream(encryptedDataStream, transform, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[InternalBufferSize];
                    int length;
                    while ((length = csDecrypt.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        dataStream.Write(buffer, 0, length);
                    }
                }
            }
        }

    }
}

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
    /// Provides functions for encrypting data with a public key and decrypting data with a private key using 
    /// asymmetrical encryption algorithms.
    /// </summary>
    public class Asymmetric
    {
        private const int MinEncryptedKeyLength = 64;
        private const int MaxEncryptedKeyLength = 1024 * 1024;

        /// <summary>
        /// Creates a new public key / private key pair.
        /// </summary>
        /// <param name="publicKey">The public part of the key</param>
        /// <param name="privateKey">The private part of the key</param>
        public void CreateKeyPair(out byte[] publicKey, out byte[] privateKey)
        {
            using (var asymmetricAlgorithm = CreateAsymmetricAlgorithm())
            {
                privateKey = asymmetricAlgorithm.ExportCspBlob(true);
                publicKey = asymmetricAlgorithm.ExportCspBlob(false);
            }
        }


        /// <summary>
        /// Encrypts the specified data stream using the specified public encryption key. Uses
        /// symmetric encryption for the data and asymmetric encryption algorithm for key exchange.
        /// </summary>
        /// <param name="data">The data stream to encrypt.</param>
        /// <param name="publicEncryptionKey">The public encrypton key to use, encoded as
        /// an RSA CSP blob.</param>
        /// <param name="encryptedDataStream">The data stream to write encrypted data to.</param>
        /// <exception cref="T:System.ArgumentNullException">data, publicKey or encryptedDataStream are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be encrypted using the publicKey.</exception>
        public void EncryptData(Stream dataStream, byte[] publicKey, Stream encryptedDataStream)
        {
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");
            if (publicKey == null)
                throw new ArgumentNullException("publicKey");
            if (encryptedDataStream == null)
                throw new ArgumentNullException("encryptedDataStream");

            // The data stream will be encrypted with a symmetric algorithm, but we encrypt the
            // symmetric secret key with an asymmetric algorithm

            Symmetric symmetric = CreateSymmetric();
            byte[] secretKey = symmetric.CreateSecretKey();

            byte[] encryptedSecretKey = EncryptAsymmetric(secretKey, publicKey);
            using (BinaryWriter writer = new BinaryWriter(encryptedDataStream))
            {
                writer.Write(encryptedSecretKey.Length);
                writer.Write(encryptedSecretKey);

                // Encrypt the data stream
                symmetric.EncryptData(dataStream, secretKey, encryptedDataStream);
            }
        }


        /// <summary>
        /// Encrypts the specified data using the specified public encryption key. Uses
        /// symmetric encryption for the data and asymmetric encryption algorithm for key exchange.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="publicKey">The public key to use, must have been previously returned as the
        /// publicKey parameter from the CreateKeyPair() function.</param>
        /// <returns>A byte array containing the encrypted data.</returns>
        /// <exception cref="T:System.ArgumentNullException">data or publicEncryptionKey are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be encrypted using the publicKey.</exception>
        public byte[] EncryptData(byte[] data, byte[] publicKey)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (publicKey == null)
                throw new ArgumentNullException("publicKey");

            using (MemoryStream resultStream = new MemoryStream(data.Length * 2))
            {
                using (MemoryStream dataStream = new MemoryStream(data, false))
                {
                    EncryptData(dataStream, publicKey, resultStream);
                }

                return resultStream.ToArray();
            }
        }


        /// <summary>
        /// Decrypts the specified encrypted data using the specified private key. Uses
        /// symmetric encryption for the data and asymmetric encryption algorithm for key exchange.
        /// </summary>
        /// <param name="encryptedDataStream">The encrypted data stream to be decrypted. The data in the stream
        /// must have been previously created by the EncryptData() function.</param>
        /// <param name="privateKey">The privateKey key to use, must have been previously returned as the
        /// privateKey parameter from the CreateKeyPair() function.</param>
        /// <param name="dataStream">The data stream into which the decrypted data will be written.</param>
        /// <exception cref="T:System.ArgumentNullException">encryptedDataStream, privateKey or dataStream are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be decrypted using the secretKey.</exception>
        /// <exception cref="T:System.IO.InvalidDataException">The data in the encryptedDataStream is invalid.</exception>
        public void DecryptData(Stream encryptedDataStream, byte[] privateKey, Stream dataStream)
        {
            if (encryptedDataStream == null)
                throw new ArgumentNullException("encryptedDataStream");
            if (privateKey == null)
                throw new ArgumentNullException("privateKey");
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");

            using (BinaryReader reader = new BinaryReader(encryptedDataStream))
            {
                int encryptedKeyLength = reader.ReadInt32();
                if (encryptedKeyLength < MinEncryptedKeyLength || encryptedKeyLength > MaxEncryptedKeyLength)
                {
                    throw new InvalidDataException("The encrypted data is invalid");
                }

                byte[] encryptedKey = new byte[encryptedKeyLength];
                reader.Read(encryptedKey, 0, encryptedKeyLength);
                byte[] secretKey = DecryptAsymmetric(encryptedKey, privateKey);

                Symmetric symmetric = CreateSymmetric();
                symmetric.DecryptData(encryptedDataStream, secretKey, dataStream);
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted data using the specified private key.
        /// </summary>
        /// <param name="encryptedData">The encrypted data to be decrypted. Must have been
        /// previously returned from the EncryptData() function.</param>
        /// <param name="privateKey">The privateKey key to use, must have been previously returned as the
        /// privateKey parameter from the CreateKeyPair() function.</param>
        /// <returns>The decrypted data.</returns>
        /// <exception cref="T:System.ArgumentNullException">encryptedData or privateKey are null.</exception>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could
        /// not be decrypted using the privateKey.</exception>
        /// <exception cref="T:System.IO.InvalidDataException">The data in encryptedData is invalid.</exception>
        public byte[] DecryptData(byte[] encryptedData, byte[] privateKey)
        {
            if (encryptedData == null)
                throw new ArgumentNullException("encryptedData");
            if (privateKey == null)
                throw new ArgumentNullException("privateKey");

            using (MemoryStream resultStream = new MemoryStream())
            {
                using (MemoryStream encryptedDataStream = new MemoryStream(encryptedData, false))
                {
                    DecryptData(encryptedDataStream, privateKey, resultStream);
                }

                return resultStream.ToArray();
            }
        }



        /// <summary>
        /// Simple asymmetric encryption
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        private byte[] EncryptAsymmetric(byte[] data, byte[] publicKey)
        {
            using (var asymmetricAlgorithm = CreateAsymmetricAlgorithm())
            {
                asymmetricAlgorithm.ImportCspBlob(publicKey);
                return asymmetricAlgorithm.Encrypt(data, false);
            }
        }

        /// <summary>
        /// Simple asymmetric decryption
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        private byte[] DecryptAsymmetric(byte[] encryptedData, byte[] privateKey)
        {
            using (var asymmetricAlgorithm = CreateAsymmetricAlgorithm())
            {
                asymmetricAlgorithm.ImportCspBlob(privateKey);
                return asymmetricAlgorithm.Decrypt(encryptedData, false);
            }
        }




        /// <summary>
        /// Creates a RSACryptoServiceProvider object.
        /// </summary>
        /// <returns>A properly initialized RSACryptoServiceProvider object.</returns>
        protected virtual RSACryptoServiceProvider CreateAsymmetricAlgorithm()
        {
            // Create CSP parameters to ensure we use the machine key store, otherwise users
            // with temporary or roaming profiles cannot import CSP blobs.
            CspParameters csp = new CspParameters();
            csp.Flags = CspProviderFlags.UseMachineKeyStore;
            csp.ProviderType = 1;		// Compatiple with RSA
            return new RSACryptoServiceProvider(csp);
        }

        /// <summary>
        /// Creates a Symmetric object that is used for encrypting and decripting the data proper.
        /// </summary>
        /// <returns>A Symmetric object</returns>
        protected virtual Symmetric CreateSymmetric()
        {
            return new Symmetric();
        }


    }
}

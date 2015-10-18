using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Marson.Vault.Core
{
    public static class VaultSecurity
    {
        private const int SaltLength = 10;
        private const int KeyBitLength = 256;

        private static byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return bytes;
        }

        public static byte[] Encrypt(string paintext, SecureString password)
        {
            var salt = GenerateSalt(SaltLength);
            byte[] encryptedBytes;
            using (var symmetric = InitSymmetric(AesManaged.Create(), password, KeyBitLength, salt))
            {
                var bytes = Encoding.UTF8.GetBytes(paintext);
                encryptedBytes = Transform(bytes, symmetric.CreateEncryptor);
            }

            return MergeBuffers(salt, encryptedBytes);
        }


        public static string Decrypt(byte[] cyphertext, SecureString password)
        {
            byte[] salt;
            byte[] encryptedBytes;
            SplitBuffer(cyphertext, SaltLength, out salt, out encryptedBytes);

            byte[] clearBytes;
            using (var symmetric = InitSymmetric(AesManaged.Create(), password, KeyBitLength, salt))
            {
                clearBytes = Transform(encryptedBytes, symmetric.CreateDecryptor);
            }

            return Encoding.UTF8.GetString(clearBytes);
        }

        private static SymmetricAlgorithm InitSymmetric(SymmetricAlgorithm algorithm, SecureString password, int keyBitLength, byte[] salt)
        {
            const int Iterations = 1000;

            using (var secureStringBytes = new SecureStringBytes(password))
            {
                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(secureStringBytes.GetBytes(), salt, Iterations))
                {
                    if (!algorithm.ValidKeySize(keyBitLength))
                    {
                        throw new InvalidOperationException("Invalid size key");
                    }

                    algorithm.Key = rfc2898DeriveBytes.GetBytes(keyBitLength / 8);
                    algorithm.IV = rfc2898DeriveBytes.GetBytes(algorithm.BlockSize / 8);
                    return algorithm;
                }
            }
        }

        private static byte[] Transform(byte[] bytes, Func<ICryptoTransform> selectCryptoTransform)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, selectCryptoTransform(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        private static byte[] MergeBuffers(byte[] buffer1, byte[] buffer2)
        {
            var result = new byte[buffer1.Length + buffer2.Length];
            Buffer.BlockCopy(buffer1, 0, result, 0, buffer1.Length);
            Buffer.BlockCopy(buffer2, 0, result, buffer1.Length, buffer2.Length);

            return result;
        }

        private static void SplitBuffer(byte[] buffer, int buffer1Length, out byte[] buffer1, out byte[] buffer2)
        {
            buffer1 = new byte[buffer1Length];
            buffer2 = new byte[buffer.Length - buffer1.Length];
            Buffer.BlockCopy(buffer, 0, buffer1, 0, buffer1.Length);
            Buffer.BlockCopy(buffer, buffer1.Length, buffer2, 0, buffer2.Length);
        }

        //private static string ConvertToUnsecureString(SecureString secstrPassword)
        //{
        //    IntPtr unmanagedString = IntPtr.Zero;
        //    try
        //    {
        //        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
        //        return Marshal.PtrToStringUni(unmanagedString);
        //    }
        //    finally
        //    {
        //        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        //    }
        //}

    }
}

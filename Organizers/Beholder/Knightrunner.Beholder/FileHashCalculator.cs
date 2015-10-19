using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Beholder
{
    /// <summary>
    /// A utility class for calculating a file hash.
    /// </summary>
    public class FileHashCalculator : IDisposable
    {
        private MD5 md5;

        public FileHashCalculator()
        {
            md5 = MD5.Create();
        }

        ~FileHashCalculator()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (md5 != null)
                {
                    md5.Dispose();
                    md5 = null;
                }
            }
        }

        /// <summary>
        /// Calculates an MD5 hash for the contents of a file.
        /// </summary>
        /// <param name="filePath">File path (absoloute or relative) of the file.</param>
        /// <returns>A string value representing the MD5 hash of the contents of the file.</returns>
        public string MD5HashFile(string filePath)
        {
            byte[] hash;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hash = md5.ComputeHash(stream);
            }
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}

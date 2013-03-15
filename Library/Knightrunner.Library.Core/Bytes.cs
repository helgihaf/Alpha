using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Knightrunner.Library.Core
{
    /// <summary>
    /// The Bytes class is a static utility class containing miscellaneous operations performed
    /// on byte[].
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        /// Determines if two byte arrays and/or their contents are equal. Nulls and empty
        /// byte arrays are supported.
        /// </summary>
        /// <param name="bytes1">First byte array to compare.</param>
        /// <param name="bytes2">Second byte array to compare.</param>
        /// <returns>True if the byte arrays are equal, false otherwise.</returns>
        public static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null)
            {
                return bytes2 == null;
            }

            if (bytes2 == null)
            {
                return false;
            }

            if (bytes1.Length != bytes2.Length)
                return false;

            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Dumps a byte array to a string containing hex numbers for each byte and a total byte
        /// length. Use for debugging/tracing. Null buffer is supported.
        /// </summary>
        /// <param name="bytes">The bytest to dump.</param>
        /// <returns>A string.</returns>
        public static string BytesToTraceString(byte[] bytes)
        {
            if (bytes == null)
            {
                return "(null)";
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X} ", b);
            }
            sb.Append(" (total " + bytes.Length + ")");
            return sb.ToString();
        }


        /// <summary>
        /// Reads all the bytes in the specifed stream and returns them as a byte array.
        /// Optimized for MemoryStream.
        /// </summary>
        /// <param name="stream">A readable stream.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            MemoryStream memoryStream = stream as MemoryStream;
            if (memoryStream != null)
                return memoryStream.ToArray();

            using (memoryStream = new MemoryStream())
            {
                int lastValue;
                do
                {
                    lastValue = stream.ReadByte();
                    if (lastValue != -1)
                    {
                        memoryStream.WriteByte((byte)lastValue);
                    }
                } while (lastValue != -1);
                return memoryStream.ToArray();
            }
        }

    }
}

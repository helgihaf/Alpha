using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace Knightrunner.Library.Core
{
    /// <summary>
    /// The Utils class TODO: Describe class here
    /// </summary>
    public static class Utilities
    {
        public static bool AreEqualBuffers(byte[] bufferA, byte[] bufferB)
        {
            bool result = false;

            if (bufferA != null && bufferB != null && bufferA.Length == bufferB.Length)
            {
                result = true;
                for (int i = 0; i < bufferA.Length; i++)
                {
                    if (bufferA[i] != bufferB[i])
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }



        public static void CopySingleFile(string sourceFilePath, string destFilePath, bool overwriteReadOnly)
        {
            if (overwriteReadOnly && File.Exists(destFilePath))
            {
                FileInfo fileInfo = new FileInfo(destFilePath);
                if (fileInfo.IsReadOnly)
                {
                    fileInfo.IsReadOnly = false;
                }
            }
            File.Copy(sourceFilePath, destFilePath, true);
        }


        public static void DisposeAndNull<T>(ref T disposable) where T : IDisposable
        {
            if (disposable != null)
            {
                disposable.Dispose();
                disposable = default(T) ;
            }
        }

        public static bool TryStringToBool(string value, out bool resultValue)
        {
            bool success = false;
            resultValue = false;

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var tempValue = value.ToUpperInvariant();
            if (tempValue == "TRUE" || tempValue == "1")
            {
                resultValue = true;
                success = true;
            }
            if (tempValue == "FALSE" || tempValue == "0")
            {
                resultValue = false;
                success = true;
            }
            
            return success;
        }

        public static bool StringToBool(string value)
        {
            bool resultValue;
            if (!TryStringToBool(value, out resultValue))
            {
                throw new ArgumentException("Invalid boolean string", "value");
            }

            return resultValue;
        }

        public static bool StringToBoolOrDefault(string value)
        {
            bool resultValue;
            if (!TryStringToBool(value, out resultValue))
            {
                resultValue = false;
            }

            return resultValue;
        }
    }
}

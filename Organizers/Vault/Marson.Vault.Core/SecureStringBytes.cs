using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Vault.Core
{
public sealed class SecureStringBytes : IDisposable
{
    private SecureString secureString;
    private byte[] bytes;

    public SecureStringBytes(SecureString secureString)
    {
        if (secureString == null)
        {
            throw new ArgumentNullException("secureString");
        }
        this.secureString = secureString;
    }

    public void Clear()
    {
        if (bytes != null)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }
            bytes = null;
        }
    }

    public void Dispose()
    {
        Clear();
    }

    public byte[] GetBytes()
    {
        if (bytes == null)
        {
            bytes = ConvertSecureStringToBytes(secureString);
        }
        return bytes;
    }

    private static byte[] ConvertSecureStringToBytes(SecureString secureString)
    {
        var result = new byte[secureString.Length * 2];
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            for (int i = 0; i < secureString.Length; i++)
            {
                result[i] = Marshal.ReadByte(valuePtr, i * 2);
                result[i + 1] = Marshal.ReadByte(valuePtr, i * 2 + 1);
            }
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }

        return result;
    }
}
}

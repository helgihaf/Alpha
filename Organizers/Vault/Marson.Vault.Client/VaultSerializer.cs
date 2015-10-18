using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Vault.Core
{
    public static class VaultSerializer
    {
        private static readonly Version CurrentVersion = new Version(1, 0);
        private const string FieldSeperator = ";";
        
        public static void Serialize(string filePath, IVaultContent vaultContent, SecureString password)
        {
            var cyphertext = Convert.ToBase64String(VaultSecurity.Encrypt(vaultContent.Text, password));
            File.WriteAllText(filePath, CurrentVersion.ToString(2) + FieldSeperator + cyphertext);
        }

        public static IVaultContent Deserialize(string filePath, SecureString password)
        {
            var allText = File.ReadAllText(filePath);
            int index = allText.IndexOf(FieldSeperator);
            if (index < 3 || index + 3 > allText.Length)
            {
                throw new FormatException("Invalid file format.");
            }

            Version fileVersion;
            if (!Version.TryParse(allText.Substring(0, index), out fileVersion))
            {
                throw new FormatException("Invalid file format.");
            }

            if (CurrentVersion.Major != fileVersion.Major || CurrentVersion.Minor != fileVersion.Minor)
            {
                throw new FormatException("Unknown file version.");
            }

            var bytes = Convert.FromBase64String(allText.Substring(index + 1));
            IVaultContent vaultContent = new VaultContent();
            vaultContent.Text = VaultSecurity.Decrypt(bytes, password);

            return vaultContent;
        }
    }
}

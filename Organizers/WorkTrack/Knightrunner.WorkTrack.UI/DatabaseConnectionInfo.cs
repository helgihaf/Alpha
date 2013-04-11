using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.WorkTrack.UI
{
    public class DatabaseConnectionInfo
    {
        private static readonly byte[] key = new byte[]
        {
            0x70, 0x21, 0xdb, 0xf6, 0x9f, 0x3a, 0x11, 0x13, 0x23, 0x16, 0xd3, 0x7d, 0xb4, 0x4a, 0x79, 0xfd, 0xb6, 0xab, 0x25,
            0x5d, 0x6c, 0xd0, 0xf6, 0x39, 0x17, 0xbe, 0xc0, 0xfa, 0xf3, 0x80, 0xb5, 0xf4
        };

        public DatabaseConnectionInfo()
        {
            UseWindowsAuthentication = true;
        }

        public string ServerName { get; set; }
        public bool UseWindowsAuthentication { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
        public string DatabaseName { get; set; }

        public bool RememberPassword { get; set; }

        public string Password
        {
            get
            {
                if (!string.IsNullOrEmpty(EncryptedPassword))
                {
                    return DecryptPassword(EncryptedPassword);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    EncryptedPassword = EncryptPassword(value);
                }
                else
                {
                    EncryptedPassword = null;
                }
            }
        }


        public string ConnectionString
        {
            get
            {
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
                builder.DataSource = ServerName;
                builder.InitialCatalog = DatabaseName;
                builder.IntegratedSecurity = UseWindowsAuthentication;
                if (!UseWindowsAuthentication)
                {
                    builder.UserID = UserName;
                    builder.Password = Password;
                }

                return builder.ToString();
            }
        }

        private static string DecryptPassword(string encryptedPassword)
        {
            var symmetric = new Knightrunner.Library.Cryptography.Symmetric();
            var decrypted = symmetric.DecryptData(Convert.FromBase64String(encryptedPassword), key);
            return Encoding.Unicode.GetString(decrypted);
        }

        private static string EncryptPassword(string password)
        {
            var symmetric = new Knightrunner.Library.Cryptography.Symmetric();
            var encrypted = symmetric.EncryptData(Encoding.Unicode.GetBytes(password), key);
            return Convert.ToBase64String(encrypted);
        }



    }
}

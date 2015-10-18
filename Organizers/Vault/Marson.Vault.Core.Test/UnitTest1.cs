using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;

namespace Marson.Vault.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldEncryptAndDecript()
        {
            string password = "ekki flýja af hólmi þótt á brattan sæki";
            string cleartext = "Einu sinni fyrir langa löngu var lítill drengur";


            var mudBytes = VaultSecurity.Encrypt(cleartext, ToSecureString(password));
            string roundTrip = VaultSecurity.Decrypt(mudBytes, ToSecureString(password));

            Assert.AreEqual(cleartext, roundTrip);
        }

        private SecureString ToSecureString(string s)
        {
            var result = new SecureString();
            foreach (var c in s)
            {
                result.AppendChar(c);
            }
            result.MakeReadOnly();

            return result;
        }


        //[TestMethod]
        //public void ShouldSerialize()
        //{
        //    var vault = new List<VaultEntry>
        //    {
        //        new VaultEntry { Description = "Facebook", UserName = "gijoe@facebook.com", Password = "I rulez", LastChanged = DateTime.Now },
        //        new VaultEntry { Description = "Hotmail", UserName = "gijoe@hotmail.com", Password = "I few rulez", LastChanged = DateTime.Now },
        //    };

        //    var s = VaultSerializer.Serialize(vault);
        //    Debug.WriteLine(s);
        //    var vault2 = new List<VaultEntry>(VaultSerializer.Deserialize(s));
        //    Assert.AreEqual(vault.Count, vault2.Count);
        //    var s2 = VaultSerializer.Serialize(vault2);
        //    Assert.AreEqual(s, s2);
        //}

    }
}

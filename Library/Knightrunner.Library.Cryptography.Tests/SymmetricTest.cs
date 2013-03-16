using Knightrunner.Library.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Knightrunner.Library.Core;
using System.Diagnostics;
using System.Text;

namespace Knightrunner.Library.Cryptography.Tests
{
    
    
    /// <summary>
    ///This is a test class for SymmetricTest and is intended
    ///to contain all SymmetricTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SymmetricTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        private readonly byte[] mainSecretKey = new byte[]
        {
            0x78, 0x73, 0x84, 0x74, 0xD6, 0xF0, 0xB5, 0x83, 0x5, 0xDB, 0x93, 0xBF, 0x3A, 0x21, 
            0xE9, 0x4B, 0x2D, 0xD6, 0x4A, 0xD8, 0x56, 0x19, 0xD0, 0x9D, 0x5C, 0x38, 0xE5, 0xC1, 
            0x9F, 0xB, 0xE6, 0x41
        };

        /// <summary>
        ///A test for EncryptData
        ///</summary>
        [TestMethod()]
        public void EncryptDataTest()
        {
            Symmetric symmetric = new Symmetric();

            // Short data test
            byte[] data = new byte[] { 7, 12, 69, 47, 249 };

            byte[] encrypted = symmetric.EncryptData(data, mainSecretKey);
            byte[] actual = symmetric.DecryptData(encrypted, mainSecretKey);
            AssertBytesAreEqual(data, actual);

            // Longer data test
            string msg =
                "Implementing the IDisposable interface can be the source of great " +
                "confusion amongst many programmers. Whenever possible, I use the following " +
                "pattern when implementing it. So far it covers most of the cases I have needed "+
                "but for a more extensive discussion see this excellent article by Scott Dorman: " +
                "www.codeproject.com/KB/cs/idisposable.aspx";
            data = Encoding.Unicode.GetBytes(msg);
            encrypted = symmetric.EncryptData(data, mainSecretKey);
            actual = symmetric.DecryptData(encrypted, mainSecretKey);
            AssertBytesAreEqual(data, actual);

            // Null argument
            data = null;
            try
            {
                encrypted = symmetric.EncryptData(data, mainSecretKey);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
            }

            // Zero data length
            data = new byte[0];
            encrypted = symmetric.EncryptData(data, mainSecretKey);
            actual = symmetric.DecryptData(encrypted, mainSecretKey);
            AssertBytesAreEqual(data, actual);

            // Single byte
            data = new byte[] { 7 };
            encrypted = symmetric.EncryptData(data, mainSecretKey);
            actual = symmetric.DecryptData(encrypted, mainSecretKey);
            AssertBytesAreEqual(data, actual);

            // Long data test
            data = new byte[4 * 1024 * 1024];
            Random random = new Random(71269);
            random.NextBytes(data);
            encrypted = symmetric.EncryptData(data, mainSecretKey);
            actual = symmetric.DecryptData(encrypted, mainSecretKey);
            AssertBytesAreEqual(data, actual);

        }

        private void AssertBytesAreEqual(byte[] expected, byte[] actual)
        {
            if (expected != null && actual != null)
            {
                Assert.AreEqual(expected.Length, actual.Length);
                for (int i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], actual[i]);
                }
            }
            else
            {
                Assert.AreEqual(expected == null, actual == null);
            }
        }

        ///// <summary>
        /////A test for DecryptData
        /////</summary>
        //[TestMethod()]
        //public void DecryptDataTest()
        //{
        //    byte[] encryptedData = null; // TODO: Initialize to an appropriate value
        //    byte[] secretKey = null; // TODO: Initialize to an appropriate value
        //    byte[] expected = null; // TODO: Initialize to an appropriate value
        //    byte[] actual;
        //    actual = symmetric.DecryptData(encryptedData, secretKey);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for CreateSecretKey
        /////</summary>
        //[TestMethod()]
        //public void CreateSecretKeyTest()
        //{
        //    byte[] expected = null; // TODO: Initialize to an appropriate value
        //    byte[] actual;
        //    actual = symmetric.CreateSecretKey();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}

using Knightrunner.Library.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Knightrunner.Library.Core.Tests
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


        /// <summary>
        ///A test for EncryptData
        ///</summary>
        [TestMethod()]
        public void EncryptDataTest()
        {

            byte[] data = new byte[] { 7, 12, 69, 47, 249 };
            byte[] secretKey = Symmetric.CreateSecretKey();

            byte[] encrypted = Symmetric.EncryptData(data, secretKey);
            byte[] actual = Symmetric.DecryptData(encrypted, secretKey);
            Assert.AreEqual(data, actual);
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
        //    actual = Symmetric.DecryptData(encryptedData, secretKey);
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
        //    actual = Symmetric.CreateSecretKey();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}

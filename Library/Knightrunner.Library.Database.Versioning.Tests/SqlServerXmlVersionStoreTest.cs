using Knightrunner.Library.Database.Versioning.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Versioning.Tests
{
    
    
    /// <summary>
    ///This is a test class for SqlServerXmlVersionStoreTest and is intended
    ///to contain all SqlServerXmlVersionStoreTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SqlServerXmlVersionStoreTest
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
        ///A test for SqlServerXmlVersionStore Constructor
        ///</summary>
        [TestMethod()]
        public void SqlServerXmlVersionStoreConstructorTest()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=lap-helgih2;Initial Catalog=WorkTrack;Integrated Security=true");
            sqlConnection.Open();
            SqlServerXmlVersionStore target = new SqlServerXmlVersionStore(sqlConnection);
            target.CachingEnabled = false;

            Version expected = null;
            Version actual;

            actual = target.Version;
            Assert.AreEqual(expected, actual);

            Version version = new Version(1, 2, 3, 4);
            target.Version = version;

            expected = version;
            actual = target.Version;
            Assert.AreEqual(expected, actual);
        }

  
        /// <summary>
    }
}

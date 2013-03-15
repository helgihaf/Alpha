using Knightrunner.Library.Database.Versioning.ResourceScripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Knightrunner.Library.Database.Versioning;

namespace Knightrunner.Library.Database.Versioning.Tests
{
    
    
    /// <summary>
    ///This is a test class for ResourceScriptScannerTest and is intended
    ///to contain all ResourceScriptScannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ResourceScriptScannerTest
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
        ///A test for CreateScriptFromResourceName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Knightrunner.Library.Database.Versioning.dll")]
        public void CreateScriptFromResourceNameTest()
        {
            string resourceName = null;
            Script expected = null;
            Script actual;
            actual = ResourceScriptScanner_Accessor.CreateScriptFromResourceName(resourceName);
            Assert.AreEqual(expected, actual);

            resourceName = string.Empty;
            expected = null;
            actual = ResourceScriptScanner_Accessor.CreateScriptFromResourceName(resourceName);
            Assert.AreEqual(expected, actual);

            resourceName = "ConsoleApplication9.Scripts.Update._001._000._00001._0000.001-Update.sql";
            expected = new Script { ScriptType = ScriptType.Update, Version = new Version(1, 0, 1, 0), Sequence = 1, Name = "Update" };
            actual = ResourceScriptScanner_Accessor.CreateScriptFromResourceName(resourceName);
            Assert.AreEqual(expected, actual);

            resourceName = "ConsoleApplication9.Scripts.Create.1.0.1.0.1-a.sql";
            expected = new Script { ScriptType = ScriptType.Create, Version = new Version(1, 0, 1, 0), Sequence = 1, Name = "a" };
            actual = ResourceScriptScanner_Accessor.CreateScriptFromResourceName(resourceName);
            Assert.AreEqual(expected, actual);

            resourceName = "ConsoleApplication9.Scripts.Create.1.0.1.0.1-a.sqlx";
            expected = null;
            actual = ResourceScriptScanner_Accessor.CreateScriptFromResourceName(resourceName);
            Assert.AreEqual(expected, actual);
        
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marson.Compare.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var engine = new Engine();
            var ble = engine.CompareDirectories(@"C:\li\Framework\Service\STS\Release\2.4\source", @"C:\li\Framework\Service\STS\Dev\2.4debug\source", new CompareOptions());
            Console.WriteLine(ble.GetType().FullName);
        }
    }
}

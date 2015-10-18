using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marson.LogEyes.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldLoadLogFile()
        {
            var view = new View(@"LogFile.log");
            var cursor = view.CreateCursor();
            cursor.Locate(Offset.End, 20);
            var lines = cursor.ReadLines(20);
            Assert.IsTrue(lines.Length >= 19);
        }
    }
}

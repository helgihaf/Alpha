using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knightrunner.Library.ExpressionParsing.Nodes;
using System.Diagnostics;

namespace Knightrunner.Library.ExpressionParsing.Tests
{
    [TestClass]
    public class ObjectEvaluationTests
    {
    
        [TestMethod]
        public void TestEvaluationObjects()
        {
            Person person = new Person
            {
                Name = "Guðlaug Edda Siggeirsdóttir",
                DateOfBirth = new DateTime(1972, 12, 25),
                Children = new[]
                {
                    new Person { Name = "Ásta Margrét Helgadóttir", DateOfBirth = new DateTime(1995, 3, 4) },
                    new Person { Name = "Hafþór Andri Helgason", DateOfBirth = new DateTime(1997, 10, 27) },
                    new Person { Name = "Halla Bryndís Helgadóttir", DateOfBirth = new DateTime(2006, 9, 25) }
                }
            };
            var evaluator = new Evaluator();
            evaluator.Resolver = new PersonResolver(person);

            object actual = evaluator.Evaluate("Person.Name");
            object expected = person.Name;
            Assert.AreEqual(expected, actual);

            actual = evaluator.Evaluate("Person.CalculateAge()");
            expected = 37;
            Assert.AreEqual(expected, actual);

            actual = evaluator.Evaluate("Person.CalculateAge()");
            expected = 37;
            Assert.IsTrue((int)actual >= (int)expected);

            actual = evaluator.Evaluate("Person.Children[0].Name");
            expected = person.Children[0].Name;
            Assert.AreEqual(expected, actual);

        }
    
    }
}

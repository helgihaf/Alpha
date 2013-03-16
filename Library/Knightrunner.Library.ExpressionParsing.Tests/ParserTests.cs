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
    public class ParserTests
    {
        [TestMethod]
        public void TestParse()
        {
            ExpressionParser parser = new ExpressionParser();
            Node node = parser.Parse("1 + 2 * 3");

            BinaryNode plusNode = node as BinaryNode;
            Assert.IsNotNull(plusNode);
            Assert.AreEqual(BinaryOperator.Add, plusNode.Operator);

            IntLiteralNode leftNode = plusNode.Left as IntLiteralNode;
            Assert.IsNotNull(leftNode);
            Assert.AreEqual(1, leftNode.Value);

            BinaryNode multiplyNode = plusNode.Right as BinaryNode;
            Assert.IsNotNull(multiplyNode);
            Assert.AreEqual(BinaryOperator.Multiply, multiplyNode.Operator);

            IntLiteralNode multLeftNode = multiplyNode.Left as IntLiteralNode;
            Assert.IsNotNull(multLeftNode);
            Assert.AreEqual(2, multLeftNode.Value);

            IntLiteralNode multRightNode = multiplyNode.Right as IntLiteralNode;
            Assert.IsNotNull(multRightNode);
            Assert.AreEqual(3, multRightNode.Value);
        }

        [TestMethod]
        public void TestVisitor()
        {
            var visitor = new WriterVisitor();
            ExpressionParser parser = new ExpressionParser();
            Node node = parser.Parse("1 + 2 * 3");
            // ( 1  Plus ( 2  Multiply  3 ))
            node.Accept(visitor);
        }


        [TestMethod]
        public void TestEvaluation()
        {
            var evaluator = new Evaluator();
            object actual = evaluator.Evaluate("1 + 2 * 3");
            Assert.IsTrue(actual is int);
            Assert.AreEqual(7, (int)actual);

            actual = evaluator.Evaluate("3 + 5 * (-2 ** 7 - 8/2)");
            Assert.IsTrue(actual is int);
            Assert.AreEqual(-657, (int)actual);
        }





    }



    public class PrintVisitor : INodeVisitor
    {
        public void Visit(ArrayMemberNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(BinaryNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(CharLiteralNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(FloatLiteralNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(FunctionNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(IdentifierNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }


        public void Visit(StringLiteralNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(UnaryNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

        public void Visit(IntLiteralNode node)
        {
            Debug.WriteLine("Visiting " + node.GetType().Name);
        }

    }


    public class WriterVisitor : INodeVisitor
    {
        public void Visit(BinaryNode node)
        {
            Debug.Write("(");
            node.Left.Accept(this);
            Debug.Write(" " + node.Operator + " ");
            node.Right.Accept(this);
            Debug.Write(")");
        }

        public void Visit(IntLiteralNode node)
        {
            Debug.Write(" " + node.Value + " ");
        }

        public void Visit(ArrayMemberNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(CharLiteralNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(FloatLiteralNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(FunctionNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(IdentifierNode node)
        {
            throw new NotImplementedException();
        }


        public void Visit(StringLiteralNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnaryNode node)
        {
            throw new NotImplementedException();
        }
    }

}

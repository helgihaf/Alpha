using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.ExpressionParsing.Nodes;

namespace Knightrunner.Library.ExpressionParsing
{
    public class Evaluator : INodeVisitor
    {
        private ExpressionParser parser = new ExpressionParser();
        private ValueStack valueStack = new ValueStack();

        public IEvaluationResolver Resolver { get; set; }

        public object Evaluate(string expression)
        {
            var node = parser.Parse(expression);
            if (node == null)
            {
                return null;
            }

            valueStack.Clear();
            node.Accept(this);

            return valueStack.Peek();
        }


        public void Visit(ArrayMemberNode node)
        {
            AssertResolver();
            List<object> indexers = new List<object>();
            foreach (var indexerNode in node.Indexers)
            {
                indexerNode.Accept(this);
                indexers.Add(valueStack.Pop());
            }

            if (!node.LateEvaluation)
            {
                valueStack.Push(Resolver.ResolveArrayMember(node.Identifier, indexers));
            }
            else
            {
                var memberInfo =
                    new MemberInfo { Name = node.Identifier, MemberType = MemberType.Array };
                memberInfo.Arguments.AddRange(indexers);
                valueStack.Push(memberInfo);
            }
        }

        public void Visit(BinaryNode node)
        {
            node.Left.Accept(this);
            node.Right.Accept(this);

            switch (node.Operator)
            {
                case BinaryOperator.Or:
                    valueStack.Or();
                    break;
                case BinaryOperator.And:
                    valueStack.And();
                    break;
                case BinaryOperator.BitwiseOr:
                    valueStack.BitwiseOr();
                    break;
                case BinaryOperator.BitwiseAnd:
                    valueStack.BitwiseAnd();
                    break;
                case BinaryOperator.IsEqual:
                    valueStack.IsEqual();
                    break;
                case BinaryOperator.IsNotEqual:
                    valueStack.IsNotEqual();
                    break;
                case BinaryOperator.IsLess:
                    valueStack.IsLess();
                    break;
                case BinaryOperator.IsGreater:
                    valueStack.IsGreater();
                    break;
                case BinaryOperator.IsLessOrEqual:
                    valueStack.IsLessOrEqual();
                    break;
                case BinaryOperator.IsGreaterOrEqual:
                    valueStack.IsGreaterOrEqual();
                    break;
                case BinaryOperator.Add:
                    valueStack.Add();
                    break;
                case BinaryOperator.Subtract:
                    valueStack.Subtract();
                    break;
                case BinaryOperator.Multiply:
                    valueStack.Multiply();
                    break;
                case BinaryOperator.Divide:
                    valueStack.Divide();
                    break;
                case BinaryOperator.Modulus:
                    valueStack.Modulus();
                    break;
                case BinaryOperator.Power:
                    valueStack.Power();
                    break;
                case BinaryOperator.InvokeMember:
                    InvokeMember();
                    break;
                default:
                    throw new NotImplementedException("Implementation mission for BinaryOperator." + node.Operator.ToString());
            }
        }

        private void InvokeMember()
        {
            AssertResolver();
            MemberInfo memberInfo = (MemberInfo)valueStack.Pop();
            object obj = valueStack.Pop();
            valueStack.Push(Resolver.ResolveMemberInvoke(obj, memberInfo));
        }

        public void Visit(CharLiteralNode node)
        {
            valueStack.Push(node.Value.ToString());
        }

        public void Visit(FloatLiteralNode node)
        {
            valueStack.Push(node.Value);
        }

        public void Visit(FunctionNode node)
        {
            List<object> parameters = new List<object>();
            if (node.Parameters != null)
            {
                foreach (var paramNode in node.Parameters)
                {
                    paramNode.Accept(this);
                    parameters.Add(valueStack.Pop());
                }
            }

            if (!node.LateEvaluation)
            {
                valueStack.Push(Resolver.ResolveFunctionCall(node.FunctionName, parameters));
            }
            else
            {
                var memberInfo =
                    new MemberInfo { Name = node.FunctionName, MemberType = MemberType.Function};
                memberInfo.Arguments.AddRange(parameters);
                valueStack.Push(memberInfo);
            }
        }

        public void Visit(IdentifierNode node)
        {
            AssertResolver();

            if (!node.LateEvaluation)
            {
                valueStack.Push(Resolver.ResolveIdentifier(node.Identifier));
            }
            else
            {
                valueStack.Push(new MemberInfo { Name = node.Identifier, MemberType = MemberType.Property });
            }
        }

        public void Visit(IntLiteralNode node)
        {
            valueStack.Push(node.Value);
        }

        public void Visit(StringLiteralNode node)
        {
            valueStack.Push(node.Value);
        }

        public void Visit(UnaryNode node)
        {
            node.Value.Accept(this);
            if (node.Operator == UnaryOperator.Not)
            {
                valueStack.Not();
            }
            else if (node.Operator == UnaryOperator.BitwiseNot)
            {
                valueStack.BitwiseNot();
            }
            else
            {
                throw new NotImplementedException("Implementation mission for UnaryOperator." + node.Operator.ToString());
            }
        }


        private void AssertResolver()
        {
            if (Resolver == null)
            {
                throw new EvaluationException("Resolver not set");
            }
        }


    }
}

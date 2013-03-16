using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Knightrunner.Library.ExpressionParsing.Nodes;

namespace Knightrunner.Library.ExpressionParsing
{
    public class ExpressionParser
    {
        private Lex lex;

        public Nodes.Node Parse(string expression)
        {
            using (TextReader reader = new StringReader(expression))
            {
                lex = new Lex();
                lex.TextReader = reader;
                lex.Next();

                return Expression();
            }
        }

        // Cycle start

        private Nodes.Node Expression()
        {
            Node node = AndExpression();
            return ExpressionRest(node);
        }

        private Node ExpressionRest(Node node)
        {
            Node result;

            if (lex.Token == Token.Or)
            {
                lex.Next();
                Node right = AndExpression();

                result = new BinaryNode { Left = node, Operator = BinaryOperator.Or, Right = right };
                result = ExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        // Cycle end

        private Node AndExpression()
        {
            Node node = LogicalOrExpression();
            return AndExpressionRest(node);
        }

        private Node AndExpressionRest(Node node)
        {
            Node result;

            if (lex.Token == Token.And)
            {
                lex.Next();
                Node right = LogicalOrExpression();

                result = new BinaryNode { Left = node, Operator = BinaryOperator.And, Right = right };
                result = AndExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node LogicalOrExpression()
        {
            Node node = LogicalAndExpression();
            return LogicalOrExpressionRest(node);
        }

        private Node LogicalOrExpressionRest(Node node)
        {
            Node result;

            if (lex.Token == Token.BitwiseOr)
            {
                lex.Next();
                Node right = LogicalAndExpression();

                result = new BinaryNode { Left = node, Operator = BinaryOperator.BitwiseOr, Right = right };
                result = LogicalOrExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node LogicalAndExpression()
        {
            Node node = BooleanEqualityExpression();
            return LogicalAndExpressionRest(node);
        }

        private Node LogicalAndExpressionRest(Node node)
        {
            Node result;

            if (lex.Token == Token.BitwiseAnd)
            {
                lex.Next();
                Node right = BooleanEqualityExpression();

                result = new BinaryNode { Left = node, Operator = BinaryOperator.BitwiseAnd, Right = right };
                result = LogicalAndExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node BooleanEqualityExpression()
        {
            Node node = BooleanCompareExpression();
            return BooleanEqualityExpressionRest(node);
        }

        private Node BooleanEqualityExpressionRest(Node node)
        {
            Node result;

            BinaryOperator op = BinaryOperator.Undefined;

            if (lex.Token == Token.Equal)
            {
                op = BinaryOperator.IsEqual;
            }
            else if (lex.Token == Token.NotEqual)
            {
                op = BinaryOperator.IsNotEqual;
            }


            if (op != BinaryOperator.Undefined)
            {
                lex.Next();
                Node right = BooleanCompareExpression();

                result = new BinaryNode { Left = node, Operator = op, Right = right };
                result = BooleanEqualityExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node BooleanCompareExpression()
        {
            Node node = TermExpression();
            return BooleanCompareExpressionExpressionRest(node);
        }

        private Node BooleanCompareExpressionExpressionRest(Node node)
        {
            Node result;

            BinaryOperator op;
            switch (lex.Token)
            {
                case Token.Less:
                    op = BinaryOperator.IsLess;
                    break;
                case Token.Greater:
                    op = BinaryOperator.IsGreater;
                    break;
                case Token.LessOrEqual:
                    op = BinaryOperator.IsLessOrEqual;
                    break;
                case Token.GreaterOrEqual:
                    op = BinaryOperator.IsGreaterOrEqual;
                    break;
                default:
                    op = BinaryOperator.Undefined;
                    break;
            }

            if (op != BinaryOperator.Undefined)
            {
                lex.Next();
                Node right = TermExpression();

                result = new BinaryNode { Left = node, Operator = op, Right = right };
                result = BooleanCompareExpressionExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }


        private Node TermExpression()
        {
            Node node = FactorExpression();
            return TermExpressionRest(node);
        }

        private Node TermExpressionRest(Node node)
        {
            Node result;

            BinaryOperator op = BinaryOperator.Undefined;

            if (lex.Token == Token.Plus)
            {
                op = BinaryOperator.Add;
            }
            else if (lex.Token == Token.Minus)
            {
                op = BinaryOperator.Subtract;
            }


            if (op != BinaryOperator.Undefined)
            {
                lex.Next();
                Node right = FactorExpression();

                result = new BinaryNode { Left = node, Operator = op, Right = right };
                result = TermExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node FactorExpression()
        {
            Node node = NotExpression();
            return FactorExpressionRest(node);
        }

        private Node FactorExpressionRest(Node node)
        {
            Node result;

            BinaryOperator op;
            switch (lex.Token)
            {
                case Token.Multiply:
                    op = BinaryOperator.Multiply;
                    break;
                case Token.Divide:
                    op = BinaryOperator.Divide;
                    break;
                case Token.Modulus:
                    op = BinaryOperator.Modulus;
                    break;
                case Token.Power:
                    op = BinaryOperator.Power;
                    break;
                default:
                    op = BinaryOperator.Undefined;
                    break;
            }

            if (op != BinaryOperator.Undefined)
            {
                lex.Next();
                Node right = NotExpression();

                result = new BinaryNode { Left = node, Operator = op, Right = right };
                result = FactorExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }

        private Node NotExpression()
        {
            Node result;

            UnaryOperator op = UnaryOperator.Undefined;

            if (lex.Token == Token.Not)
            {
                op = UnaryOperator.Not;
            }
            else if (lex.Token == Token.BitwiseNot)
            {
                op = UnaryOperator.BitwiseNot;
            }


            if (op != UnaryOperator.Undefined)
            {
                lex.Next();
                Node node = MemberExpression();

                result = new UnaryNode { Operator = op, Value = node };
            }
            else
            {
                result = MemberExpression();
            }

            return result;
        }

        private Node MemberExpression()
        {
            Node node = LeafExpression();
            return MemberExpressionRest(node);
        }

        private Node MemberExpressionRest(Node node)
        {
            Node result;

            if (lex.Token == Token.Dot)
            {
                lex.Next();
                Node right = LateBindingIdentifierExpression();

                result = new BinaryNode { Left = node, Operator = BinaryOperator.InvokeMember, Right = right };
                result = MemberExpressionRest(result);
            }
            else
            {
                result = node;
            }

            return result;
        }


        private Node LeafExpression()
        {
            Node result;

            switch (lex.Token)
            {
                case Token.Identifier:
                {
                    string identifier = lex.Lexeme;
                    lex.Next(false);
                    result = IdentifierRest(identifier, false);
                    break;
                }
                case Token.IntLiteral:
                {
                    int value = int.Parse(lex.Lexeme);
                    lex.Next(false);
                    result = new IntLiteralNode { Value = value };
                    break;
                }

                case Token.FloatLiteral:
                {
                    double value = double.Parse(lex.Lexeme);
                    lex.Next(false);
                    result = new FloatLiteralNode { Value = value };
                    break;
                }

                case Token.CharLiteral:
                {
                    char value = char.Parse(lex.Lexeme);
                    lex.Next(false);
                    result = new CharLiteralNode { Value = value };
                    break;
                }

                case Token.StringLiteral:
                {
                    string value = lex.Lexeme;
                    lex.Next(false);
                    result = new StringLiteralNode { Value = value };
                    break;
                }

                case Token.OpenParentheses:
                    lex.Next();
                    result = Expression();
                    if (lex.Token != Token.ClosedParentheses)
                    {
                        throw CreateErrorExpectedToken(Token.ClosedParentheses);
                    }
                    lex.Next();
                    break;

                default:
                    throw CreateErrorUnexpectedToken(lex.Token, lex.Lexeme);
            }

            return result;
        }

        private Node IdentifierRest(string identifier, bool lateEvaluation)
        {
            Node result;
            List<Node> nodeList = null;

            if (lex.Token == Token.OpenParentheses)
            {
                //
                // Function call
                //
                lex.Next();
                if (lex.Token != Token.ClosedParentheses)
                {
                    nodeList = ExpressionList();
                    if (nodeList == null)
                    {
                        throw CreateErrorExpected("expression");
                    }
                    if (lex.Token != Token.ClosedParentheses)
                    {
                        throw CreateErrorExpectedToken(Token.ClosedParentheses);
                    }
                }
                lex.Next();
                var functionNode =
                    new FunctionNode { FunctionName = identifier, LateEvaluation = lateEvaluation };
                functionNode.Parameters.AddRange(nodeList);
                result = functionNode;
            }
            else if (lex.Token == Token.OpenBracket)
            {
                lex.Next();
                nodeList = ExpressionList();
                if (nodeList == null)
                {
                    throw CreateErrorExpected("array-indexing expression");
                }
                if (lex.Token != Token.ClosedBracket)
                {
                    throw CreateErrorExpectedToken(Token.ClosedBracket);
                }
                lex.Next();
                var arrayMemberNode = new ArrayMemberNode { Identifier = identifier, LateEvaluation = lateEvaluation };
                arrayMemberNode.Indexers.AddRange(nodeList);
                result = arrayMemberNode;
            }
            else
            {
                result = new IdentifierNode { Identifier = identifier, LateEvaluation = lateEvaluation };
            }

            return result;
        }

        private List<Node> ExpressionList()
        {
            // Some friendly cheating: Check for known closures (strictly speaking we shouldn't know any closures...)
            if (lex.Token == Token.ClosedParentheses || lex.Token == Token.ClosedBracket)
            {
                lex.Next();
                return null;
            }

            List<Node> list = new List<Node>();
            bool more = true;
            while (more)
            {
                Node node = Expression();
                if (node == null)
                {
                    throw CreateErrorExpected("expression");
                }
                list.Add(node);
                more = lex.Token == Token.Comma;
                if (more)
                {
                    lex.Next();
                }
            }

            return list;
        }




        private Node LateBindingIdentifierExpression()
        {
            if (lex.Token != Token.Identifier)
            {
                throw CreateErrorExpected("identifier");
            }

            string identifier = lex.Lexeme;
            lex.Next(false);
            return IdentifierRest(identifier, true);
        }


        private Exception CreateErrorExpectedToken(Token token)
        {
            string expected = lex.GetTokenString(token);
            if (expected == null)
            {
                expected = token.ToString();
            }
            return CreateErrorExpected(expected);
        }

        private Exception CreateErrorUnexpectedToken(Token token, string lexeme)
        {
            return CreateError("Unexpected token [" + token.ToString() + "]: \"" + lexeme + "\"");
        }

        private Exception CreateErrorExpected(string expected)
        {
            return CreateError("Expected " + expected + ".");
        }

        private Exception CreateError(string msg)
        {
            return new ParseException(msg, lex.Line, lex.Column);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public interface INodeVisitor
    {
        void Visit(ArrayMemberNode node);
        void Visit(BinaryNode node);
        void Visit(CharLiteralNode node);
        void Visit(FloatLiteralNode node);
        void Visit(FunctionNode node);
        void Visit(IdentifierNode node);
        void Visit(IntLiteralNode node);
        void Visit(StringLiteralNode node);
        void Visit(UnaryNode node);
    }
}

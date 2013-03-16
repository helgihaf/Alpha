using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class IntLiteralNode : Node
    {
        public int Value { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}

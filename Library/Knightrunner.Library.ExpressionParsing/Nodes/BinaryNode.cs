using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class BinaryNode : Node
    {
        public BinaryOperator Operator { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

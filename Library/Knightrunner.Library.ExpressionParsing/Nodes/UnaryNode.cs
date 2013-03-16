using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class UnaryNode : Node
    {
        public UnaryOperator Operator { get; set; }
        public Node Value { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}

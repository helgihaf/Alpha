using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class IdentifierNode : Node
    {
        public string Identifier { get; set; }
        public bool LateEvaluation { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}

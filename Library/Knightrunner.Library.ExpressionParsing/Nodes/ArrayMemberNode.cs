using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class ArrayMemberNode : Node
    {
        public ArrayMemberNode()
        {
            Indexers = new List<Node>();
        }

        public string Identifier { get; set; }
        public List<Node> Indexers { get; private set; }
        public bool LateEvaluation { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public class FunctionNode : Node
    {
        public FunctionNode()
        {
            Parameters = new List<Node>();
        }

        public string FunctionName { get; set; }
        public List<Node> Parameters { get; private set; }
        public bool LateEvaluation { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}

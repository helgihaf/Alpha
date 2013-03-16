using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public abstract class Node
    {
        public abstract void Accept(INodeVisitor visitor);
    }
}

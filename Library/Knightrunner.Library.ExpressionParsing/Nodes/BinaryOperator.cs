using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Nodes
{
    public enum BinaryOperator
    {
        Undefined,

        Or,
        And,
        BitwiseOr,
        BitwiseAnd,
        IsEqual,
        IsNotEqual,
        IsLess,
        IsGreater,
        IsLessOrEqual,
        IsGreaterOrEqual,
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulus,
        Power,
        InvokeMember,
    }
}

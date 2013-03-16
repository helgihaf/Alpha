using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing
{
    public enum Token
    {
        // Internal
        None,
        EndOfSource,
        Error,
        Comments,

        // Symbols
        Or,
        And,
        Not,
        BitwiseOr,
        BitwiseAnd,
        BitwiseNot,
        Plus,
        Minus,
        Multiply,
        Divide,
        Modulus,
        Power,
        Equal,
        NotEqual,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual,
        OpenParentheses,
        ClosedParentheses,
        OpenBracket,
        ClosedBracket,
        Dot,
        Comma,

        // Complex
        Identifier,
        FunctionIdentifier,
        IntLiteral,
        FloatLiteral,
        StringLiteral,
        CharLiteral,

    }
}

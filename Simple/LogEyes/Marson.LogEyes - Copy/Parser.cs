using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.LogEyes
{
    public class Parser
    {
        private readonly ILexer lexer;

        public Parser(ILexer lexer)
        {
            this.lexer = lexer;
        }

        public string NextLine()
        {
            Token token = lexer.Next();
            while (token != Token.String && token != Token.EndOfStream)
            {
                token = lexer.Next();
            }

            if (token == Token.String)
            {
                return lexer.Lexeme();
            }
            else
            {
                return null;
            }
        }

    }
}

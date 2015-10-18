using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.LogEyes
{
    public interface ILexer
    {
        Token Next();
        string Lexeme();
    }
}

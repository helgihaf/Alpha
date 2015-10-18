using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEscaper
{
    class CSharpStringEscaper
    {
        public string Escape(string s)
        {
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                if (c == '"')
                {
                    sb.Append("\\\"");
                }
                else if (c == '\t')
                {
                    sb.Append("\\t");
                }
                else if (c == '\n')
                {
                    sb.Append("\\n");
                }
                else if (c == '\r')
                {
                    sb.Append("\\r");
                }
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing
{
    public class MemberInfo
    {
        public MemberInfo()
        {
            Arguments = new List<object>();
        }

        public string Name { get; set; }
        public MemberType MemberType { get; set; }
        public List<object> Arguments { get; private set; }
    }

    public enum MemberType
    {
        Property,
        Function,
        Array,
    }
}

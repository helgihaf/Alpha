using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing
{
    [Serializable]
    public class ParseException : Exception
    {
        public ParseException()
        {
        }
        
        public ParseException(string message)
            : base(message)
        {
        }
        
        public ParseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ParseException(string message, int line, int column)
            : base(message)
        {
            this.Line = line;
            this.Column = column;
        }


        protected ParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Line", Line);
            info.AddValue("Column", Column);
        }

        public int Line { get; private set; }
        public int Column { get; private set; }
    }
}

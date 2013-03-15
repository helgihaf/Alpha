using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    [Serializable]
    public class ScriptVersionException : Exception
    {
        public ScriptVersionException() { }
        public ScriptVersionException(string message) : base(message) { }
        public ScriptVersionException(string message, Exception inner) : base(message, inner) { }
        protected ScriptVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

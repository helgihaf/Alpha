using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    [Serializable]
    public class DataSchemaException : Exception
    {
        public DataSchemaException() { }
        public DataSchemaException(string message) : base(message) { }
        public DataSchemaException(string message, Exception inner) : base(message, inner) { }
        protected DataSchemaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

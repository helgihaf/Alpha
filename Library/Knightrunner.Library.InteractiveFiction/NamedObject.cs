using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.InteractiveFiction
{
    /// <summary>
    /// A uniquely named object
    /// </summary>
    public class NamedObject : INamed
    {
        public string Name { get; set; }
    }
}

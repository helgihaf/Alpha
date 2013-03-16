using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.InteractiveFiction
{
    public class Location : NamedObject
    {
        public string Description { get; set; }

        public DynamicProperties Properties { get; private set; }
    }
}

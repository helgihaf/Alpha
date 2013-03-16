using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.InteractiveFiction
{
    public class Connector
    {
        public Location LocationA { get; set; }
        public Location LocationB { get; set; }

        public bool IsDuplex { get; set; }
        public bool Enabled { get; set; }

        public DynamicProperties Properties { get; private set; }
    }
}

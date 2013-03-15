using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema.Project
{
    public class Transformation
    {
        public string Name { get; set; }
        public ISchemaTransformation Method { get; set; }
        public string TargetSystemName { get; set; }

        public XElement Element { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Database.Schema.PetaPoco
{
    public class PetaPocoTransformation : ISchemaTransformation
    {
        public void Transform(DataSchema dataSchema, string name, TargetSystem targetSystem, System.Xml.Linq.XElement transformationElement, IScriptDocumentGeneratorFactory docGenFactory)
        {
            throw new NotImplementedException();
        }
    }
}

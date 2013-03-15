using Knightrunner.Library.Database.Schema.Verification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema
{
    public interface ISchemaTransformation
    {
        void Transform(TransformationContext context);
    }

    public class TransformationContext
    {
        public DataSchema DataSchema { get; set; }
        public string Name { get; set; }
        public TargetSystem TargetSystem { get; set; }
        public XElement TransformationElement { get; set; }
        public IScriptDocumentGeneratorFactory DocGenFactory { get; set; }
        public IVerificationContext VerificationContext { get; set; }
    }
}

using Knightrunner.Library.Database.Schema.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema.Oracle
{
    public class OracleSchemaTransformation : ISchemaTransformation
    {
        public void Transform(TransformationContext context)
        {
            var gen = new OracleScriptGenerator();
            gen.TargetSystem = context.TargetSystem;
            gen.DataSchema = context.DataSchema;

            var accessor = new SimpleXmlValueAccessor(context.TransformationElement, DataSchemaProject.TargetNamespace);
            gen.DatabaseSchemaName = accessor.GetString("DatabaseSchemaName");
            var docGenName = accessor.GetStringOrDefault("ScriptDocumentGenerator");
            if (!string.IsNullOrEmpty(docGenName))
            {
                gen.ScriptDocumentGenerator = context.DocGenFactory.Create(docGenName);
            }

            var outputFilePath = accessor.GetStringOrDefault("OutputFile");
            using (FileStream stream = new FileStream(outputFilePath, FileMode.Create))
            {
                gen.Generate(stream);
            }
        }
    }
}

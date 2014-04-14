using Knightrunner.Library.Database.Schema.Verification;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Knightrunner.Library.Database.Schema.Project
{
    public class DataSchemaProject
    {
        public const string TargetNamespace = "http://www.knightrunner.com/Library/Database/SchemaProject";

        public DataSchemaProject()
        {
            InputFiles = new List<string>();
            Options = new ProjectOptions();
            Transformations = new List<Transformation>();
        }

        public string Name { get; set; }
        public List<string> InputFiles { get; private set; }
        public ProjectOptions Options { get; private set; }
        public List<Transformation> Transformations { get; private set; }
        
        public void LoadFrom(string projectFilePath, IVerificationContext context, ISchemaTransformationFactory schemaTransformationFactory)
        {
            string baseDirectory = Path.GetDirectoryName(projectFilePath);

            using (StreamReader reader = new StreamReader(projectFilePath))
            {
                var serializer = new XmlSerializer(typeof(Parsing.Project));
                var parsedProject = (Parsing.Project)serializer.Deserialize(reader);

                foreach (var parsedInputFile in parsedProject.ProjectSettings.InputFiles)
                {
                    if (VerifyString(context, parsedInputFile.path, "Project cannot have an input file with an empty path."))
                    {
                        InputFiles.Add(ExpandFilePath(parsedInputFile.path, baseDirectory));
                    }
                }

                Options.PrimaryKeyFormatString = parsedProject.ProjectSettings.Options.PrimaryKeyFormatString;
                Options.ForeignKeyFormatString = parsedProject.ProjectSettings.Options.ForeignKeyFormatString;
                Options.UniqueIndexFormatString = parsedProject.ProjectSettings.Options.UniqueIndexFormatString;
                Options.IndexFormatString = parsedProject.ProjectSettings.Options.IndexFormatString;

                foreach (var parsedTransformation in parsedProject.Transformations)
                {
                    VerifyString(context, parsedTransformation.name, "Transformation cannot have an empty name.");
                    VerifyString(context, parsedTransformation.method, "Transformation cannot have an empty method.");
                    VerifyString(context, parsedTransformation.target, "Transformation cannot have an empty method.");
                    if (context.HasErrors)
                    {
                        continue;
                    }

                    var transformation = new Transformation();
                    transformation.Name = parsedTransformation.name;
                    try
                    {
                        transformation.Method = schemaTransformationFactory.Create(parsedTransformation.method);
                    }
                    catch (ArgumentException ex)
                    {
                        context.Add(new VerificationMessage(Severity.Error, ex.Message));
                        continue;
                    }
                    transformation.TargetSystemName = parsedTransformation.target;
                    transformation.Element = new XElement(XName.Get("Transformation", TargetNamespace),
                        new XAttribute("name", parsedTransformation.name),
                        new XAttribute("method", parsedTransformation.method),
                        new XAttribute("target", parsedTransformation.target),
                        parsedTransformation.Any.Select(e => XElement.Parse(e.OuterXml))
                    );
                    Transformations.Add(transformation);
                }
            }

            Name = Path.GetFileNameWithoutExtension(projectFilePath);
        }

        private string ExpandFilePath(string filePath, string baseDirectory)
        {
            if (Path.IsPathRooted(filePath))
            {
                return filePath;
            }

            return Path.Combine(baseDirectory, filePath);
        }

        private bool VerifyString(IVerificationContext context, string s, string errorText)
        {
            bool result = !string.IsNullOrWhiteSpace(s);
            if (!result)
            {
                context.Add(new VerificationMessage(Severity.Error, errorText));
            }
            
            return result;
        }

        public void Build(IVerificationContext context, IScriptDocumentGeneratorFactory docGenFactory)
        {
            var dataSchema = CompileSchema(context);
            if (dataSchema == null)
            {
                return;
            }

            dataSchema.Verify(context);
            if (context.Entries.Count != 0)
            {
                return;
            }

            context.Add(new VerificationMessage(Severity.Info, Properties.Resources.DataSchemaValidated));

            foreach (var transformation in Transformations)
            {
                var targetSystem = dataSchema.TargetSystems[transformation.TargetSystemName];
                if (targetSystem == null)
                {
                    context.Add(new VerificationMessage(Severity.Error, string.Format(CultureInfo.CurrentCulture, Properties.Resources.TargetSystemNameInTransformationNotFound, transformation.TargetSystemName, transformation.Name)));
                    continue;
                }
                context.Add(new VerificationMessage(Severity.Info, string.Format(CultureInfo.CurrentCulture, Properties.Resources.RunningTransformation, transformation.Name)));
                var transformationContext = new TransformationContext
                {
                    DataSchema = dataSchema,
                    Name = transformation.Name,
                    TargetSystem = targetSystem,
                    TransformationElement = transformation.Element,
                    DocGenFactory = docGenFactory,
                    VerificationContext = context
                };
                transformation.Method.Transform(transformationContext);
            }
        }

        public DataSchema CompileSchema(IVerificationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            DataSchema dataSchema = new DataSchema();
            dataSchema.Name = Name;

            foreach (var filePath in InputFiles)
            {
                var inputFilePath = Path.GetFullPath(filePath);

                if (!File.Exists(inputFilePath))
                {

                    context.Add(new VerificationMessage(Severity.Error, string.Format(CultureInfo.CurrentCulture, Properties.Resources.InputFileNotFound, inputFilePath)));
                    return null;
                }

                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    dataSchema.LoadDataSchemaFile(reader, context);
                }
            }

            if (!string.IsNullOrEmpty(Options.PrimaryKeyFormatString))
            {
                dataSchema.NameFormats.PrimaryKeyFormatString = Options.PrimaryKeyFormatString;
            }
            if (!string.IsNullOrEmpty(Options.ForeignKeyFormatString))
            {
                dataSchema.NameFormats.ForeignKeyFormatString = Options.ForeignKeyFormatString;
            }
            if (!string.IsNullOrEmpty(Options.UniqueIndexFormatString))
            {
                dataSchema.NameFormats.UniqueIndexFormatString = Options.UniqueIndexFormatString;
            }
            if (!string.IsNullOrEmpty(Options.IndexFormatString))
            {
                dataSchema.NameFormats.IndexFormatString = Options.IndexFormatString;
            }

            return dataSchema;
        }
    }
}

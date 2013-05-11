using Knightrunner.Library.Database.Schema.Project;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema.PetaPoco
{
    public class PetaPocoTransformation : ISchemaTransformation
    {
        public void Transform(TransformationContext context)
        {
            var accessor = new SimpleXmlValueAccessor(context.TransformationElement, DataSchemaProject.TargetNamespace);

            var generator = new PetaPocoGenerator();
            generator.UseColumnAttribute = accessor.GetBool("UseColumnAttribute");
            generator.UseExplicitColumnsAttribute = accessor.GetBool("UseExplicitColumnsAttribute");
            generator.UsePrimaryKeyAttribute = accessor.GetBool("UsePrimaryKeyAttribute");
            generator.UseTableNameAttribute = accessor.GetBool("UseTableNameAttribute");
            generator.UsingNamespaces.AddRange(SplitUsings(accessor.GetStringOrDefault("UsingNamespaces")));
            generator.ConvertTableNamesToSingularClassNames = accessor.GetBool("ConvertTableNamesToSingularClassNames");
            generator.DatabaseSchemaName = accessor.GetString("DatabaseSchemaName");
            generator.CodeNamespace = accessor.GetString("CodeNamespace");
            generator.TargetSystem = context.TargetSystem;

            var directoryPath = accessor.GetString("DirectoryPath");
            foreach (Table table in context.DataSchema.Tables)
            {
                string fileName = GetFileName(table.Name, generator.ConvertTableNamesToSingularClassNames);
                string filePath = Path.Combine(directoryPath, fileName);
                FileInfo fileInfo = new FileInfo(filePath);
                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
                if (!(fileInfo.Exists && fileInfo.IsReadOnly))
                {
                    using (var writer = new StreamWriter(filePath))
                    {
                        if (context.VerificationContext != null)
                        {
                            context.VerificationContext.Add(new Verification.VerificationMessage(Verification.Severity.Info, string.Format(Properties.Resources.GeneratingFile, filePath)));
                        }
                        generator.Generate(table, writer);
                    }
                }
                else
                {
                    context.VerificationContext.Add(new Verification.VerificationMessage(Verification.Severity.Warning, string.Format(CultureInfo.CurrentCulture, Properties.Resources.FileIsReadOnly, filePath)));
                }
            }
        }

        private string[] SplitUsings(string usings)
        {
            if (usings == null)
            {
                return null;
            }
            return usings.Split(';', ',');
        }

        private static string GetFileName(string tableName, bool convertToSingular)
        {
            return PetaPocoGenerator.GenerateClassName(tableName, convertToSingular) + ".cs";
        }

        //public static void SplitFullTableName(string fullTableName, out string tableOwner, out string tableName)
        //{
        //    string[] tableNameParts = fullTableName.Split('.');
        //    if (tableNameParts.Length != 2)
        //    {
        //        throw new ArgumentException("Invalid table name " + fullTableName + ", expected owner.table");
        //    }
        //    tableOwner = tableNameParts[0];
        //    tableName = tableNameParts[1];
        //}

    }
}

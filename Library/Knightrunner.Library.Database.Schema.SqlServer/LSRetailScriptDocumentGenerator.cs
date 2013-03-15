using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.SqlServer
{
    public class LSRetailScriptDocumentGenerator : IScriptDocumentGenerator
    {
        public void WriteDocumentation(string databaseSchemaName, System.IO.StreamWriter writer, Table table)
        {
            bool lineWritten = false;

            if (!string.IsNullOrWhiteSpace(table.Description))
            {
                writer.WriteLine("exec spDB_SetTableDescription_1_0 '" + table.Name + "','" + SqlServerScriptGenerator.StringToScript(table.Description) + "';");
                lineWritten = true;
            }

            foreach (Column column in table.Columns)
            {
                if (!string.IsNullOrWhiteSpace(column.Description))
                {
                    writer.WriteLine("exec spDB_SetFieldDescription_1_0 '" + table.Name + "', '" + column.Name + "', '" + SqlServerScriptGenerator.StringToScript(column.Description) + "';");
                    lineWritten = true;
                }
            }

            if (lineWritten)
            {
                writer.WriteLine();
            }
        }
    }
}

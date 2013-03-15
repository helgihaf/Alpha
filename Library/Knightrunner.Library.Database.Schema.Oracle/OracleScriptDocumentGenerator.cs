using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Database.Schema.Oracle
{
    public class OracleScriptDocumentGenerator : IScriptDocumentGenerator
    {
        public void WriteDocumentation(string databaseSchemaName, System.IO.StreamWriter writer, Table table)
        {
            bool anythingWritten = false;

            if (!string.IsNullOrEmpty(table.Description))
            {
                writer.WriteLine("comment on table {0}.{1} is '{2}';", Identifier(databaseSchemaName), Identifier(table.Name), table.Description);
                anythingWritten = true;
            }

            foreach (Column column in table.Columns)
            {
                if (!string.IsNullOrEmpty(column.Description))
                {
                    writer.WriteLine("comment on column {0}.{1}.{2} is '{3}';", Identifier(databaseSchemaName), Identifier(table.Name), Identifier(column.Name), column.Description);
                    anythingWritten = true;
                }
            }

            if (anythingWritten)
            {
                OracleScriptGenerator.WriteGo(writer);
            }
        }

        private string Identifier(string text)
        {
            return OracleScriptGenerator.Identifier(text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;

namespace Knightrunner.Library.Database.Schema
{
    public class Index
    {
        private string name;

        public Index(Table table)
        {
            Table = table;
            Columns = new List<Column>();
        }

        public Table Table { get; private set; }
        public string Name
        {
            get
            {
                if (name == null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        if (i > 0)
                            sb.Append("_");
                        sb.Append(Columns[i].Name);
                    }

                    string formatString;
                    if (this.IsUnique)
                    {
                        formatString = Table.DataSchema.NameFormats.UniqueIndexFormatString;
                    }
                    else
                    {
                        formatString = Table.DataSchema.NameFormats.IndexFormatString;
                    }
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, 
                        formatString, 
                        Table.Name, 
                        sb.ToString()
                        );
                }
                else
                {
                    return name;
                }
            }
            set
            {
                name = value;
            }
        }
        public List<Column> Columns { get; private set; }
        public bool IsUnique { get; set; }


        public void Verify(IVerificationContext context, string tableName)
        {
            if (Table == null)
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.IndexNoTable));
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.IndexNameEmpty));
            }

            HashSet<string> columnNames = new HashSet<string>();
            foreach (Column column in Columns)
            {
                if (columnNames.Contains(column.Name))
                {
                    context.Add(new VerificationTableMessage(Severity.Error, tableName, 
                        string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnDuplicated, column.Name)));
                }
                else
                {
                    columnNames.Add(column.Name);
                }
            }
        }
    }
}

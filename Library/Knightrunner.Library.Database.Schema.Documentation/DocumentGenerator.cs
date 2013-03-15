using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Knightrunner.Library.Database.Schema.SqlServer;
using System.Globalization;

namespace Knightrunner.Library.Database.Schema.Documentation
{
    public class DocumentGenerator
    {
        public DataSchema DataSchema { get; set; }
        public TargetSystem DatabaseTargetSystem { get; set; }
        public IColumnTypeMapper DatabaseColumnTypeMapper { get; set; }

        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public string TableSchemaName { get; set; }
        public string CssFile { get; set; }


        public void Generate()
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                throw new InvalidOperationException("FileName can not be empty");
            }

            string filePath = Path.Combine(DirectoryPath, FileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                Generate(fileStream);
            }

        }



        public void Generate(Stream outStream)
        {
            if (outStream == null)
                throw new ArgumentNullException("outStream");

            if (DataSchema == null)
                throw new InvalidOperationException("DatabaseSchema property has not been set");

            if (DatabaseTargetSystem == null)
                throw new InvalidOperationException("DatabaseTargetSystem property has not been set");

            if (DatabaseColumnTypeMapper == null)
                throw new InvalidOperationException("ColumnTypeMapper property has not been set");

            Verification.VerificationContext context = new Verification.VerificationContext();
            DataSchema.Verify(context);
            if (context.HasErrors)
            {
                throw new InvalidOperationException("The DataSchema has verification errors");
            }

            using (StreamWriter writer = new StreamWriter(outStream, Encoding.UTF8))
            {
                WritePrologue(writer);
                foreach (Table table in DataSchema.Tables)
                {
                    WriteTable(writer, table);
                }
                WriteEpilogue(writer);
            }
        }


        private void WritePrologue(StreamWriter writer)
        {
            writer.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" >");
            writer.WriteLine("<head>");
            if (CssFile != null)
            {
                writer.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + CssFile + "\">");
            }
            writer.WriteLine("<title>" + ToHtml(DataSchema.Name) + "</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
        }

        private void WriteEpilogue(StreamWriter writer)
        {
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        private void WriteTable(StreamWriter writer, Table table)
        {
            writer.WriteLine("<h2>" + ToHtml(table.Name) + "</h2>");
            writer.WriteLine("<p>" + ToHtml(table.Description) + "</p>");
            writer.WriteLine("<table border=\"1\">");
            writer.WriteLine("<tr>");
            writer.WriteLine("<th align=\"left\"> </th>");
            writer.WriteLine("<th align=\"left\">Name</th>");
            writer.WriteLine("<th align=\"left\">Data Type</th>");
            writer.WriteLine("<th align=\"left\">Description</th>");
            writer.WriteLine("</tr>");

            foreach (Column column in table.Columns)
            {
                writer.WriteLine("<tr>");

                var foreignKeyIndicator = FindForeignKeyIndicator(table, column);

                WriteIndicators(writer, table, column, foreignKeyIndicator);
                WriteColumnName(writer, column);
                WriteDataType(writer, column, foreignKeyIndicator);
                WriteDescription(writer, column);

                writer.WriteLine("</tr>");
            }

            writer.WriteLine("</table>");
            writer.WriteLine("<br/>");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Knightrunner.Library.Database.Schema.Documentation.DocumentGenerator.ToHtml(System.String)")]
        private void WriteDescription(StreamWriter writer, Column column)
        {
            string enumName = string.Empty;
            if (column.ColumnType.EnumTypeName != null)
            {
                enumName = column.ColumnType.EnumTypeName.Replace("global::", string.Empty) + ".";
            }

            string description = column.Description;

            writer.WriteLine("<td>" + ToHtml((enumName + " " + description).Trim()) + "</td>");
        }

        private void WriteIndicators(StreamWriter writer, Table table, Column column, ForeignKeyIndicator foreignKeyIndicator)
        {
            List<string> indicators = new List<string>();
            if (column.InPrimaryKey)
            {
                indicators.Add("PK");
            }

            string indexIndicator = FindIndexIndicator(table, column);
            if (indexIndicator != null)
            {
                indicators.Add(ToHtml(indexIndicator));
            }

            if (foreignKeyIndicator != null)
            {
                indicators.Add(ToHtml(foreignKeyIndicator.Identifier));
            }


            writer.Write("<td>");
            if (indicators.Count > 0)
            {
                writer.Write(string.Join("<br/>", indicators));
            }
            writer.WriteLine("</td>");
        }

        private void WriteDataType(StreamWriter writer, Column column, ForeignKeyIndicator foreignKeyIndicator)
        {
            string columnType = DatabaseColumnTypeMapper.GetColumnTypeString(DatabaseTargetSystem, column);
            columnType = columnType.Replace(" NOT NULL", "");
            string dataType = ToHtml(columnType);
            if (foreignKeyIndicator != null)
            {
                dataType += "<br/>" + ToHtml(foreignKeyIndicator.ToTableText);
            }
            writer.WriteLine("<td>" + dataType + "</td>");
        }

        private void WriteColumnName(StreamWriter writer, Column column)
        {
            string columnName = ToHtml(column.Name);
            string columnDisplayName;
            
            if (!column.CanBeNull)
                columnDisplayName = "<b>" + columnName + "</b>";
            else
                columnDisplayName = columnName;

            if (column.InPrimaryKey)
                columnDisplayName = "<u>" + columnDisplayName + "</u>";

            writer.WriteLine("<td>" + columnDisplayName + "</td>");
        }

        private ForeignKeyIndicator FindForeignKeyIndicator(Table table, Column column)
        {
            for (int i = 0; i < table.ForeignKeys.Count; i++)
            {
                foreach (var columnPair in table.ForeignKeys[i].Columns)
                {
                    if (columnPair.FromColumn == column)
                    {
                        return new ForeignKeyIndicator
                        {
                            Identifier = "FK" + (i + 1).ToString(CultureInfo.InvariantCulture),
                            ToTableText = "\u2192" + table.ForeignKeys[i].ToTable.Name
                        };
                    }
                }
            }

            return null;
        }

        private string FindIndexIndicator(Table table, Column column)
        {
            for (int i = 0; i < table.Indices.Count; i++)
            {
                foreach (var indexColumn in table.Indices[i].Columns)
                {
                    if (indexColumn == column)
                    {
                        string indicator = (i + 1).ToString(CultureInfo.InvariantCulture);
                        if (table.Indices[i].IsUnique)
                        {
                            return "U" + indicator;
                        }
                        else
                        {
                            return "I" + indicator;
                        }
                    }
                }
            }

            return null;
        }

        private string ToHtml(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (c == '<')
                {
                    sb.Append("&lt;");
                }
                else if (c == '>')
                {
                    sb.Append("&gt;");
                }
                else if (c == '&')
                {
                    sb.Append("&amp;");
                }
                else if (c == '\'')
                {
                    sb.Append("&#039");
                }
                else if (c == '"')
                {
                    sb.Append("&quot;");
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }


        private class ForeignKeyIndicator
        {
            public string Identifier { get; set; }
            public string ToTableText { get; set; }
        }


    }
}

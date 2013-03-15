using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Database.Schema.Oracle
{
    public class OracleScriptGenerator
    {
        public DataSchema DataSchema { get; set; }
        public string DatabaseSchemaName { get; set; }
        public ScriptCasing IdentifierCasing { get; set; }
        public TargetSystem TargetSystem { get; set; }
        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public IScriptDocumentGenerator ScriptDocumentGenerator { get; set; }

        private OracleColumnTypeMapper columnTypeMapper = new OracleColumnTypeMapper();

        public void Generate()
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                throw new InvalidOperationException("FileName can not be empty");
            }

            if (string.IsNullOrWhiteSpace(DatabaseSchemaName))
            {
                throw new InvalidOperationException("DatabaseSchemaName missing");
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

            if (TargetSystem == null)
                throw new InvalidOperationException("TargetSystem property has not been set");

            Verification.VerificationContext context = new Verification.VerificationContext();
            DataSchema.Verify(context);
            if (context.HasErrors)
            {
                throw new InvalidOperationException("The DataSchema has verification errors");
            }

            using (StreamWriter writer = new StreamWriter(outStream))
            {
                WriteHeader(writer);

                foreach (Table table in DataSchema.Tables)
                {
                    if (table.Settings.GetValueAsBool(TargetSystem.Name, "Ignore"))
                        continue;

                    var primaryKeyGeneratedColumn = GetDbGeneratedPrimaryKeyColumn(table);
                    if (primaryKeyGeneratedColumn != null)
                    {
                        WriteSequence(writer, table);
                    }
                    WriteTable(writer, table);
                    WriteGo(writer);

                    WriteNonUniqueIndices(writer, table);

                    if (ScriptDocumentGenerator != null)
                    {
                        ScriptDocumentGenerator.WriteDocumentation(DatabaseSchemaName, writer, table);
                    }

                    if (primaryKeyGeneratedColumn != null)
                    {
                        WriteSequenceTrigger(writer, table, primaryKeyGeneratedColumn);
                        WriteGo(writer);
                    }
                }

                //foreach (Table table in DataSchema.Tables)
                //{
                //    if (table.Settings.GetValueAsBool(TargetSystem.Name, "Ignore"))
                //        continue;
                //    WriteReferences(writer, table);
                //    WriteGo(writer);
                //}
            }
        }


        private void WriteSequenceTrigger(StreamWriter writer, Table table, Column primaryKeyGeneratedColumn)
        {
            const string triggerText =
@"create or replace trigger {0}.{1}
 before
  insert or update
 on {0}.{2}
referencing new as new old as old
 for each row
 when (nvl(new.{3},0) = 0)
begin
  select {0}.{4}.nextval into :new.{3} from dual;
end;";
            
            writer.WriteLine(triggerText,
                Identifier(DatabaseSchemaName),
                Identifier(SequenceTriggerName(table.Name)),
                Identifier(table.Name),
                Identifier(primaryKeyGeneratedColumn.Name),
                Identifier(SequenceName(table.Name)),
                Identifier(primaryKeyGeneratedColumn.Name));
        }

        internal static void WriteGo(StreamWriter writer)
        {
            writer.WriteLine("/");
        }

        private void WriteNonUniqueIndices(StreamWriter writer, Table table)
        {
            bool anythingWritten = false;

            foreach (Index index in table.Indices)
            {
                if (!index.IsUnique)
                {
                    WriteNonUniqueIndex(writer, index);
                    anythingWritten = true;
                }
            }

            if (anythingWritten)
            {
                WriteGo(writer);
            }
        }

        private void WriteNonUniqueIndex(StreamWriter writer, Index index)
        {
            writer.Write("create index {0}.{1} on {0}.{2} (", Identifier(DatabaseSchemaName), index.Name, index.Table.Name);
            bool hasColumns = false;
            foreach (Column column in index.Columns)
            {
                if (hasColumns)
                {
                    writer.Write(", ");
                }
                writer.Write(Identifier(column.Name));
                hasColumns = true;
            }
            writer.WriteLine(");");
            writer.WriteLine();
        }

        private void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("--");
            writer.WriteLine("-- Autogenerated Oracle script for database schema " + DataSchema.Name);
            writer.WriteLine("-- Generated at " + DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteLine("-- " + ClassAndAssemblyInfo());
            writer.WriteLine();
        }

        private void WriteSequence(StreamWriter writer, Table table)
        {
            foreach (Column column in table.Columns)
            {
                if (column.InPrimaryKey && column.ColumnType.IsDbGenerated)
                {
                    writer.WriteLine("create sequence {0}.{1};", Identifier(DatabaseSchemaName), Identifier(SequenceName(table.Name)));
                }
            }
        }

        private string SequenceName(string tableName)
        {
            // TODO: Make this an option
            return string.Format(CultureInfo.InvariantCulture, "{0}_seq", tableName);
        }

        private string SequenceTriggerName(string tableName)
        {
            // TODO: Make this an option
            return string.Format(CultureInfo.InvariantCulture, "{0}_new", tableName);
        }

        private void WriteTable(StreamWriter writer, Table table)
        {
            writer.WriteLine("create table {0}.{1}", Identifier(DatabaseSchemaName), Identifier(table.Name));
            writer.WriteLine("(");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (i > 0)
                {
                    writer.WriteLine(",");
                }
                Column column = table.Columns[i];
                writer.Write("\t" + Identifier(column.Name) + " ");
                writer.Write(columnTypeMapper.GetColumnTypeString(TargetSystem, column));
            }

            WritePrimaryKey(writer, table);
            WriteUniqueIndices(writer, table);
            WriteReferences(writer, table);

            writer.WriteLine();
            writer.WriteLine(");");
        }

        private void WriteReferences(StreamWriter writer, Table table)
        {
            foreach (ForeignKey foreignKey in table.ForeignKeys)
            {
                writer.WriteLine(",");
                writer.Write("\tconstraint {0} foreign key (", Identifier(table.ForeignKeyName(foreignKey)));
                for (int i = 0; i < foreignKey.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        writer.Write(", ");
                    }
                    writer.Write(Identifier(foreignKey.Columns[i].FromColumn.Name));
                }
                writer.Write(") references {0}.{1} (", Identifier(DatabaseSchemaName), Identifier(foreignKey.ToTable.Name));
                for (int i = 0; i < foreignKey.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        writer.Write(", ");
                    }
                    writer.Write(Identifier(foreignKey.Columns[i].ToColumn.Name));
                }
                writer.Write(")");
            }
        }

        private void WriteUniqueIndices(StreamWriter writer, Table table)
        {
            foreach (var index in table.Indices)
            {
                if (index.IsUnique)
                {
                    writer.WriteLine(",");
                    writer.Write("\tconstraint {0} unique (", Identifier(index.Name));
                    for (int i = 0; i < index.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            writer.Write(", ");
                        }
                        writer.Write(Identifier(index.Columns[i].Name));
                    }
                    writer.Write(")");
                }
            }
        }

        private void WritePrimaryKey(StreamWriter writer, Table table)
        {
            // Write primary key constraints
            if (table.HasPrimaryKey)
            {
                writer.WriteLine(",");
                writer.Write("\tconstraint {0} primary key (", Identifier(table.PrimaryKeyName));
                bool hasColumns = false;
                foreach (Column column in table.Columns)
                {
                    if (column.InPrimaryKey)
                    {
                        if (hasColumns)
                        {
                            writer.Write(", ");
                        }
                        writer.Write(Identifier(column.Name));
                        hasColumns = true;
                    }
                }
                writer.Write(")");
            }
        }


        internal static string Identifier(string text)
        {
            return text;
            //return string.Format("\"{0}\"", text);
        }

        private Column GetDbGeneratedPrimaryKeyColumn(Table table)
        {
            Column keyColumn = null;
            foreach (Column column in table.Columns)
            {
                if (column.InPrimaryKey && column.ColumnType.IsDbGenerated)
                {
                    if (keyColumn != null)
                    {
                        throw new ApplicationException("Cannot have two db generated primary key columns in the same table");
                    }
                    keyColumn = column;
                }
            }

            return keyColumn;
        }



        private string ClassAndAssemblyInfo()
        {
            return this.GetType().FullName + ", " + this.GetType().Assembly.FullName;
        }

    }
}

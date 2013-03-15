using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;
using Knightrunner.Library.Core.Collections;
using System.Globalization;

namespace Knightrunner.Library.Database.Schema
{
    public class Table : DynamicKeyedItem<string>
    {
        private string primaryKeyName;

        public Table()
        {
            Columns = new ColumnCollection();
            Indices = new List<Index>();
            ForeignKeys = new List<ForeignKey>();
            Settings = new TableSettings();
        }

        internal object Parent { get; set; }

        public DataSchema DataSchema { get; internal set; }

        public string Name
        {
            get { return Key; }
            set { Key = value; }
        }

        public string Description { get; set; }

        /// <summary>
        /// Name of the type used for this table in LINQ
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Name to use when this type is used as a member in another class.
        /// </summary>
        public string MemberName { get; set; }

        public ColumnCollection Columns { get; private set; }

        public string PrimaryKeyName
        {
            get
            {
                if (primaryKeyName == null)
                {
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, DataSchema.NameFormats.PrimaryKeyFormatString, Name);
                }
                else
                {
                    return primaryKeyName;
                }
            }
            set
            {
                primaryKeyName = value;
            }
        }

        public bool HasPrimaryKey
        {
            get
            {
                foreach (Column column in Columns)
                {
                    if (column.InPrimaryKey)
                        return true;
                }

                return false;
            }
        }

        public string ForeignKeyName(ForeignKey foreignKey)
        {
            if (foreignKey.Name != null)
            {
                return foreignKey.Name;
            }
            else
            {
                return string.Format
                (
                    CultureInfo.InvariantCulture,
                    DataSchema.NameFormats.ForeignKeyFormatString,
                    this.Name, 
                    foreignKey.ToTable.Name, 
                    foreignKey.Columns[0].FromColumn.Name, 
                    foreignKey.Columns[0].ToColumn.Name
                );
            }
        }

        public List<Index> Indices { get; private set; }

        public List<ForeignKey> ForeignKeys { get; private set; }

        public TableSettings Settings { get; private set; }

        public void Verify(IVerificationContext context)
        {
            if (DataSchema == null)
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.TableNoSchema));
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.TableNameEmpty));
            }

            foreach (Column column in Columns)
            {
                column.Verify(context, Name);
                if (column.ColumnType == null)
                {
                    if (IndexOfForeignKey(column) == -1)
                    {
                        context.Add(new VerificationTableMessage(Severity.Error, this.Name, column.Name, Properties.Resources.ColumnNoColumnType));
                    }
                }
            }

            // Ensure uniqueness of column names
            HashSet<string> columnNames = new HashSet<string>();
            foreach (Column column in Columns)
            {
                if (columnNames.Contains(column.Name))
                {
                    context.Add(new VerificationTableMessage(Severity.Error, Name, 
                        string.Format(CultureInfo.CurrentCulture, Properties.Resources.ColumnAlreadyDefined, column.Name)));
                }
                else
                {
                    columnNames.Add(column.Name);
                }
            }

            if (!HasPrimaryKey)
            {
                context.Add(new VerificationTableMessage(Severity.Warning, Name, Properties.Resources.TableNoPrimaryKey));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(PrimaryKeyName))
                {
                    context.Add(new VerificationTableMessage(Severity.Error, Name, Properties.Resources.TablePrimaryKeyEmpty));
                }
            }

            foreach (Index index in Indices)
            {
                index.Verify(context, Name);
            }
        }

        public int IndexOfForeignKey(Column column)
        {
            for (int i = 0; i < ForeignKeys.Count; i++)
            {
                var foreignKey = ForeignKeys[i];
                foreach (var columnPair in foreignKey.Columns)
                {
                    if (column == columnPair.FromColumn)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}

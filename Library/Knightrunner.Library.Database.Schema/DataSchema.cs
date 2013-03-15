using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Reflection;
using System.Xml.Serialization;
using System.Configuration;
using System.Diagnostics;
using Knightrunner.Library.Database.Schema.Verification;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class DataSchema 
    {
        private const string schemaResourceName = "Knightrunner.Library.Database.Schema.Parsing.Database.xsd";
        private const string targetNamespace = "http://www.knightrunner.com/Library/Database/Schema";

        public DataSchema()
        {
            TargetSystems = new TargetSystemCollection(this);
            ColumnTypes = new ColumnTypeCollection(this);
            Tables = new TableCollection(this);
            NameFormats = new NameFormats();
        }

        public string Name { get; set; }

        public TargetSystemCollection TargetSystems { get; private set; }
        public ColumnTypeCollection ColumnTypes { get; private set; }
        public TableCollection Tables { get; private set; }
        public NameFormats NameFormats { get; private set; } 


        public void LoadDataSchemaFile(TextReader reader, IVerificationContext context)
        {
            Parsing.DataSchema parsedSchema;
            XmlSerializer serializer = new XmlSerializer(typeof(Parsing.DataSchema));
            parsedSchema = (Parsing.DataSchema)serializer.Deserialize(reader);

            ApplyParsedSchema(parsedSchema, context);
        }


        private static void SplitReference(string reference, out string toTableName, out string toColumnName)
        {
            if (string.IsNullOrEmpty(reference))
            {
                throw new ArgumentNullException("reference");
            }

            int index = reference.IndexOf('.');
            if (index == -1 || index == reference.Length - 1)
            {
                throw new ArgumentException("Invalid reference string");
            }

            toTableName = reference.Substring(0, index);
            toColumnName = reference.Substring(index + 1);
        }

        /// <summary>
        /// Finds or creates a column type that is a reference to the specified columnType.
        /// </summary>
        private ColumnType GetReferenceToColumnType(ColumnType columnType)
        {
            string columnTypeName = "$ReferenceTo" + columnType.Name;
            ColumnType referenceColumnType = ColumnTypes[columnTypeName];
            if (referenceColumnType == null)
            {
                referenceColumnType = new ColumnType();
                referenceColumnType.Name = columnTypeName;
                referenceColumnType.CanBeNull = true;
                referenceColumnType.IsDbGenerated = false;
                referenceColumnType.EnumTypeName = columnType.EnumTypeName;
                referenceColumnType.IsDbGenerated = false;
                referenceColumnType.MaxLength = columnType.MaxLength;
                referenceColumnType.Precision = columnType.Precision;
                referenceColumnType.Scale = columnType.Scale;
                foreach (var sourceTarget in columnType.Targets)
                {
                    Target target = new Target();
                    target.TargetSystem = sourceTarget.TargetSystem;
                    if (!string.IsNullOrWhiteSpace(sourceTarget.DataTypeWhenReferenced))
                    {
                        target.DataType = sourceTarget.DataTypeWhenReferenced;
                    }
                    else
                    {
                        target.DataType = sourceTarget.DataType;
                    }
                    target.DotNetType = sourceTarget.DotNetType;
                    target.DotNetTypeNullable = sourceTarget.DotNetTypeNullable;
                    referenceColumnType.Targets.Add(target);
                }
                ColumnTypes.Add(referenceColumnType);
            }

            return referenceColumnType;
        }

        //private static XmlSchemaSet GetSchema()
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    using (var reader = new StreamReader(assembly.GetManifestResourceStream(schemaResourceName)))
        //    {
        //        XmlSchemaSet schemas = new XmlSchemaSet();
        //        schemas.Add(targetNamespace, XmlReader.Create(reader));
        //    }
        //}


        public void Verify(IVerificationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.DataSchemaNameEmpty));
            }

            foreach (var target in this.TargetSystems)
            {
                target.Verify(context);
            }

            foreach (var columnType in this.ColumnTypes)
            {
                columnType.Verify(context);
            }

            foreach (var table in this.Tables)
            {
                table.Verify(context);
            }


        }

        public void Save(string filePath)
        {
            Parsing.DataSchema parsedSchema = SchemaToParsedSchema(this);
            XmlSerializer serializer = new XmlSerializer(typeof(Parsing.DataSchema));
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    stream = null;      // writer now owns stream
                    serializer.Serialize(writer, parsedSchema);
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }



        private void ApplyParsedSchema(Parsing.DataSchema parsedSchema, IVerificationContext context)
        {
            const string PropertyMacroDefault = "(default)";

            // Column types
            foreach (Parsing.DataSchemaColumnType parsedColumnType in parsedSchema.ColumnTypes)
            {
                ColumnType columnType = new ColumnType();

                columnType.Name = parsedColumnType.name;
                columnType.Description = parsedColumnType.description;
                
                if (parsedColumnType.baseType != null)
                {
                    ColumnType baseType = this.ColumnTypes[parsedColumnType.baseType];
                    if (baseType == null)
                    {
                        context.Add(new VerificationMessage(Verification.Severity.Error,
                            string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnTypeBaseTypeUnkown, parsedColumnType.name, parsedColumnType.baseType)));
                        continue;
                    }
                    columnType.BaseType = baseType;
                    Debug.Assert(columnType.BaseType != null);
                }

                // Set default values when no base type
                if (columnType.BaseType == null)
                {
                    columnType.CanBeNull = false;
                    columnType.IsDbGenerated = false;
                }

                if (parsedColumnType.canBeNullSpecified)
                {
                    columnType.CanBeNull = parsedColumnType.canBeNull;
                }

                if (parsedColumnType.isDbGeneratedSpecified)
                {
                    columnType.IsDbGenerated = parsedColumnType.isDbGenerated;
                }

                if (parsedColumnType.maxLengthSpecified)
                {
                    columnType.MaxLength = parsedColumnType.maxLength;
                }

                if (parsedColumnType.enumType != null)
                {
                    columnType.EnumTypeName = parsedColumnType.enumType;
                }

                if (parsedColumnType.precisionSpecified)
                {
                    columnType.Precision = parsedColumnType.precision;
                }

                if (parsedColumnType.scaleSpecified)
                {
                    columnType.Scale = parsedColumnType.scale;
                }

                if (parsedColumnType.Target != null)
                {
                    foreach (var parsedTarget in parsedColumnType.Target)
                    {
                        TargetSystem targetSystem = this.TargetSystems[parsedTarget.name];
                        if (targetSystem == null)
                        {
                            targetSystem = new TargetSystem { Name = parsedTarget.name };
                            this.TargetSystems.Add(targetSystem);
                        }

                        Target target = new Target
                        {
                            TargetSystem = targetSystem,
                            DataType = parsedTarget.dataType,
                            DataTypeWhenReferenced = parsedTarget.dataTypeWhenReferenced,
                            DotNetType = parsedTarget.dotNetType,
                            DotNetTypeNullable = parsedTarget.dotNetTypeNullable
                        };
                        if (parsedTarget.ExtendedProperties != null && parsedTarget.ExtendedProperties.Length > 0)
                        {
                            target.ExtendedProperties.Clear();
                            foreach (var property in parsedTarget.ExtendedProperties)
                            {
                                target.ExtendedProperties.Add(property.name, property.Value);
                            }
                        }

                        try
                        {
                            columnType.Targets.Add(target); 
                        }
                        catch (ArgumentException)
                        {
                            context.Add(new VerificationMessage(Severity.Error,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnTypeTargetDuplicates, target.TargetSystem.Name, columnType.Name)));
                        }
                    }
                }

                columnType.Verify(context);
                if (!this.ColumnTypes.Contains(columnType.Name))
                {
                    this.ColumnTypes.Add(columnType);
                }
                else
                {
                    context.Add(new VerificationMessage(Severity.Error, 
                        string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnTypeDuplicates, columnType.Name)));
                }

            }

            if (parsedSchema.Tables != null)
            {
                // Tables: First pass - references are skipped until second pass.
                foreach (var parsedTable in parsedSchema.Tables)
                {
                    Table table = new Table();
                    table.Name = parsedTable.name;

                    try
                    {
                        this.Tables.Add(table);
                    }
                    catch (ArgumentException)
                    {
                        context.Add(new VerificationTableMessage(Severity.Error, table.Name, Properties.Resources.SchemaTableExists));
                    }

                    table.Description = parsedTable.description;

                    // Columns
                    foreach (var parsedColumn in parsedTable.Columns)
                    {
                        Column column = new Column();
                        column.Name = parsedColumn.name;
                        column.Description = parsedColumn.description;

                        if (parsedColumn.canBeNullSpecified)
                        {
                            column.CanBeNull = parsedColumn.canBeNull;
                        }

                        if (parsedColumn.inPrimaryKeySpecified)
                        {
                            column.InPrimaryKey = parsedColumn.inPrimaryKey;
                        }

                        if (!string.IsNullOrWhiteSpace(parsedColumn.columnType))
                        {
                            ColumnType columnType = this.ColumnTypes[parsedColumn.columnType];
                            if (columnType != null)
                            {
                                column.ColumnType = columnType;
                            }
                            else
                            {
                                context.Add(new VerificationTableMessage(Severity.Error, table.Name, column.Name, 
                                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnTypeUnknown, parsedColumn.columnType)));
                            }
                        }

                        table.Columns.Add(column);
                    }

                    // Indices
                    if (parsedTable.Indices != null)
                    {
                        foreach (var parsedIndex in parsedTable.Indices)
                        {
                            Index index = new Index(table);
                            if (!string.IsNullOrWhiteSpace(parsedIndex.name))
                            {
                                index.Name = parsedIndex.name;
                            }
                            if (parsedIndex.uniqueSpecified)
                            {
                                index.IsUnique = parsedIndex.unique;
                            }

                            foreach (var parsedIndexColumn in parsedIndex.Column)
                            {
                                Column column = table.Columns[parsedIndexColumn.name];
                                if (column != null)
                                {
                                    index.Columns.Add(column);
                                }
                                else
                                {
                                    context.Add(new VerificationTableMessage(Severity.Error, table.Name,
                                        string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.IndexColumnUnknown, parsedIndexColumn.name)));
                                }
                            }
                            table.Indices.Add(index);
                        }
                    }
                }

                // Tables: Second pass - resolve references
                foreach (var parsedTable in parsedSchema.Tables)
                {
                    Table table = this.Tables[parsedTable.name];

                    foreach (var parsedColumn in parsedTable.Columns)
                    {
                        if (!string.IsNullOrWhiteSpace(parsedColumn.references))
                        {
                            Column column = table.Columns[parsedColumn.name];
                            string toTableName;
                            string toColumnName;
                            SplitReference(parsedColumn.references, out toTableName, out toColumnName);

                            Table toTable = this.Tables[toTableName];
                            if (toTable == null)
                            {
                                context.Add(new VerificationTableMessage(Severity.Error, table.Name, column.Name,
                                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ReferencedTableNotExists,
                                        parsedColumn.references)));
                                continue;
                            }
                            Column toColumn = toTable.Columns[toColumnName];
                            if (toColumn == null)
                            {
                                context.Add(new VerificationTableMessage(Severity.Error, table.Name, column.Name,
                                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ReferencedColumnNotExists, parsedColumn.references)));
                                continue;
                            }
                            ForeignKey foreignKey = new ForeignKey();
                            foreignKey.FromTable = table;
                            foreignKey.ToTable = toTable;
                            foreignKey.Columns.Add(new ForeignKey.ColumnPair { FromColumn = column, ToColumn = toColumn });
                            if (!string.IsNullOrEmpty(parsedColumn.referenceName))
                            {
                                foreignKey.Name = parsedColumn.referenceName;
                            }
                            table.ForeignKeys.Add(foreignKey);

                            column.ColumnType = this.GetReferenceToColumnType(toColumn.ColumnType);
                            column.CanBeNull = false;
                            if (parsedColumn.canBeNullSpecified)
                                column.CanBeNull = parsedColumn.canBeNull;

                            // Association properties
                            foreignKey.AssociationProperty = new AssociationProperty();
                            if (parsedColumn.AssociationCode != null)
                            {
                                if (parsedColumn.AssociationCode.Child != null)
                                {
                                    ParsedAssociationEndPointToNative(parsedColumn.AssociationCode.Child, foreignKey.AssociationProperty.Child);
                                }
                                else
                                {
                                    foreignKey.AssociationProperty.Child = null;
                                }
                                ParsedAssociationEndPointToNative(parsedColumn.AssociationCode.Parent, foreignKey.AssociationProperty.Parent);
                            }
                            else
                            {
                                // Check the quick-attributes for association properties
                                if (string.IsNullOrEmpty(parsedColumn.childProperty))
                                {
                                    foreignKey.AssociationProperty.Child = null;
                                }
                                else
                                {
                                    if (string.Compare(parsedColumn.childProperty, PropertyMacroDefault, StringComparison.Ordinal) != 0)
                                    {
                                        foreignKey.AssociationProperty.Child.Name = parsedColumn.childProperty;
                                    }
                                    else
                                    {
                                        // Using default
                                        foreignKey.AssociationProperty.Child.Name = null;
                                    }
                                }

                                if (string.IsNullOrEmpty(parsedColumn.parentProperty))
                                {
                                    foreignKey.AssociationProperty.Parent = null;
                                }
                                else
                                {
                                    if (string.Compare(parsedColumn.parentProperty, PropertyMacroDefault, StringComparison.Ordinal) != 0)
                                    {
                                        foreignKey.AssociationProperty.Parent.Name = parsedColumn.parentProperty;
                                    }
                                    else
                                    {
                                        // Using default
                                        foreignKey.AssociationProperty.Parent.Name = null;
                                    }
                                }
                            }
                        }
                    }   // end foreach (var parsedColumn in parsedTable.Columns)

                    // Foreign keys
                    if (parsedTable.ForeignKeys != null)
                    {
                        foreach (var parsedForeignKey in parsedTable.ForeignKeys)
                        {
                            ForeignKey foreignKey = new ForeignKey();
                            foreignKey.Name = parsedForeignKey.name;
                            foreignKey.FromTable = table;

                            Table toTable = this.Tables[parsedForeignKey.toTable];
                            if (toTable == null)
                            {
                                context.Add(new VerificationTableMessage(Severity.Error, table.Name,
                                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ForeignKeyTableNotExists, parsedForeignKey.toTable)));
                                continue;
                            }

                            foreignKey.ToTable = toTable;

                            foreach (var columnPair in parsedForeignKey.ColumnPairs)
                            {
                                Column fromColumn = table.Columns[columnPair.from];
                                if (fromColumn == null)
                                {
                                    context.Add(new VerificationTableMessage(Severity.Error, table.Name,
                                        string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ForeignKeyFromColumnNotExists, columnPair.from)));
                                    continue;
                                }

                                Column toColumn = toTable.Columns[columnPair.to];
                                if (toColumn == null)
                                {
                                    context.Add(new VerificationTableMessage(Severity.Error, table.Name,
                                        string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ForeignKeyToColumnNotExists, columnPair.to)));
                                    continue;
                                }

                                foreignKey.Columns.Add(new ForeignKey.ColumnPair { FromColumn = fromColumn, ToColumn = toColumn });
                            }

                            table.ForeignKeys.Add(foreignKey);
                        }
                    }

                    // Table settings
                    if (parsedTable.Settings != null)
                    {
                        foreach (var parsedSetting in parsedTable.Settings)
                        {
                            if (!this.TargetSystems.Contains(parsedSetting.target))
                            {
                                context.Add(new VerificationTableMessage(Severity.Error, table.Name,
                                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.TableSettingsTargetNotExists, parsedSetting.target)));
                                continue;
                            }
                            table.Settings.Add(parsedSetting.target, parsedSetting.property, parsedSetting.value);
                        }
                    }

                }
            }
        }


        private static void ParsedAssociationEndPointToNative(Parsing.associationEndPoint parsedEndPoint, AssociationEndPoint endPoint)
        {
            if (parsedEndPoint.accessSpecified)
            {
                endPoint.Access = (PropertyAccess)parsedEndPoint.access;
            }
            if (parsedEndPoint.inheritanceModifierSpecified)
            {
                endPoint.InheritanceModifier = (PropertyInheritanceModifier)parsedEndPoint.inheritanceModifier;
            }
            endPoint.Name = parsedEndPoint.name;
        }


        private static Parsing.DataSchema SchemaToParsedSchema(DataSchema dataSchema)
        {
            Parsing.DataSchema parsedSchema = new Parsing.DataSchema();

            // Column types
            if (dataSchema.ColumnTypes.Count > 0)
            {
                parsedSchema.ColumnTypes = new Parsing.DataSchemaColumnType[dataSchema.ColumnTypes.Count];
                int columnTypeIndex = 0;
                foreach (var columnType in dataSchema.ColumnTypes)
                {
                    Parsing.DataSchemaColumnType parsedColumnType = new Parsing.DataSchemaColumnType();
                    parsedColumnType.name = columnType.Name;

                    if (columnType.BaseType != null)
                        parsedColumnType.baseType = columnType.BaseType.Name;

                    if (columnType.CanBeNull)
                    {
                        parsedColumnType.canBeNullSpecified = true;
                        parsedColumnType.canBeNull = columnType.CanBeNull;
                    }

                    if (columnType.IsDbGenerated)
                    {
                        parsedColumnType.isDbGeneratedSpecified = true;
                        parsedColumnType.isDbGenerated = columnType.IsDbGenerated;
                    }

                    if (columnType.MaxLength.HasValue)
                    {
                        parsedColumnType.maxLengthSpecified = true;
                        parsedColumnType.maxLength = columnType.MaxLength.Value;
                    }

                    parsedColumnType.enumType = columnType.EnumTypeName;

                    if (columnType.Precision.HasValue)
                    {
                        parsedColumnType.precisionSpecified = true;
                        parsedColumnType.precision = Convert.ToByte(columnType.Precision.Value);
                    }

                    if (columnType.Scale.HasValue)
                    {
                        parsedColumnType.scaleSpecified = true;
                        parsedColumnType.scale = Convert.ToByte(columnType.Scale.Value);
                    }

                    parsedColumnType.Target = new Parsing.DataSchemaColumnTypeTarget[columnType.Targets.Count];
                    int targetIndex = 0;
                    foreach (var target in columnType.Targets)
                    {
                        Parsing.DataSchemaColumnTypeTarget parsedTarget = new Parsing.DataSchemaColumnTypeTarget
                        {
                            name = target.TargetSystem.Name,
                            dataType = target.DataType,
                            dataTypeWhenReferenced = target.DataTypeWhenReferenced
                        };
                        parsedColumnType.Target[targetIndex++] = parsedTarget;
                    }

                    parsedSchema.ColumnTypes[columnTypeIndex++] = parsedColumnType;
                }
            }


            // Tables
            if (dataSchema.Tables.Count > 0)
            {
                parsedSchema.Tables = new Parsing.DataSchemaTable[dataSchema.Tables.Count];
                int tableIndex = 0;
                foreach (var table in dataSchema.Tables)
                {
                    Parsing.DataSchemaTable parsedTable = new Parsing.DataSchemaTable
                    {
                        name = table.Name,
                    };

                    // Columns
                    if (table.Columns.Count > 0)
                    {
                        parsedTable.Columns = new Parsing.DataSchemaTableColumn[table.Columns.Count];
                        for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                        {
                            Column column = table.Columns[columnIndex];
                            Parsing.DataSchemaTableColumn parsedColumn = new Parsing.DataSchemaTableColumn
                            {
                                name = column.Name,
                            };

                            if (column.CanBeNull)
                            {
                                parsedColumn.canBeNullSpecified = true;
                                parsedColumn.canBeNull = column.CanBeNull;
                            }

                            if (column.InPrimaryKey)
                            {
                                parsedColumn.inPrimaryKeySpecified = true;
                                parsedColumn.inPrimaryKey = column.InPrimaryKey;
                            }

                            // TODO: Following code should generate ForeignKeys elements later
                            //if (column.ReferencesTable != null)
                            //{
                            //    parsedColumn.references = column.ReferencesTable.Name + "." + column.ReferencesColumn.Name;
                            //}
                            //else
                            //{
                            //    parsedColumn.columnType = column.ColumnType.Name;
                            //}

                            //if (column.AssociationProperty != null)
                            //{
                            //    var parsedAssociationCode = new Parsing.DataSchemaTableColumnAssociationCode();
                            //    if (column.AssociationProperty.Child != null)
                            //    {
                            //        NativeAssociationEndPointToParsed(column.AssociationProperty.Child, parsedAssociationCode.Child);
                            //    }
                            //    else
                            //    {
                            //        parsedAssociationCode.Child = null;
                            //    }

                            //    NativeAssociationEndPointToParsed(column.AssociationProperty.Parent, parsedAssociationCode.Parent);
                            //}

                            parsedTable.Columns[columnIndex] = parsedColumn;
                        }
                    }

                    // Indices
                    if (table.Indices.Count > 0)
                    {
                        parsedTable.Indices = new Parsing.DataSchemaTableIndex[table.Indices.Count];
                        for (int indexIndex = 0; indexIndex < table.Indices.Count; indexIndex++)
                        {
                            Index index = table.Indices[indexIndex];
                            Parsing.DataSchemaTableIndex parsedIndex = new Parsing.DataSchemaTableIndex();
                            if (index.Name != null)
                            {
                                parsedIndex.name = index.Name;
                            }

                            if (index.IsUnique)
                            {
                                parsedIndex.uniqueSpecified = true;
                                parsedIndex.unique = index.IsUnique;
                            }

                            parsedIndex.Column = new Parsing.DataSchemaTableIndexColumn[index.Columns.Count];
                            for (int columnIndex = 0; columnIndex < index.Columns.Count; columnIndex++)
                            {
                                var column = index.Columns[columnIndex];
                                Parsing.DataSchemaTableIndexColumn parsedIndexColumn = new Parsing.DataSchemaTableIndexColumn
                                {
                                    name = column.Name
                                };

                                parsedIndex.Column[columnIndex] = parsedIndexColumn;
                            }

                            parsedTable.Indices[indexIndex] = parsedIndex;
                        }
                    }

                    parsedSchema.Tables[tableIndex++] = parsedTable;

                    //if (table.ForeignKeys.Count > 0)
                    //{
                    //    parsedTable.ForeignKeys = new Parsing.DataSchemaTableForeignKey[table.ForeignKeys.Count];
                    //    for (int fkIndex = 0; fkIndex < table.ForeignKeys.Count; fkIndex++)
                    //    {
                    //        ForeignKey foreignKey = table.ForeignKeys[fkIndex];
                    //        Parsing.DataSchemaTableForeignKey parsedForeignKey = new Parsing.DataSchemaTableForeignKey();
                    //        if (foreignKey != null)
                    //        {
                    //            parsedForeignKey.name = foreignKey.Name;
                    //        }
                    //        parsedForeignKey.  !!! ÉG ER HÉR
                    //}
                }
            }

            return parsedSchema;
        }

        private static void NativeAssociationEndPointToParsed(AssociationEndPoint endPoint, Parsing.associationEndPoint parsedEndPoint)
        {
            if (endPoint.Access != PropertyAccess.Public)
            {
                parsedEndPoint.accessSpecified = true;
                parsedEndPoint.access = (Parsing.propertyAccess)endPoint.Access;
            }

            if (endPoint.InheritanceModifier != PropertyInheritanceModifier.None)
            {
                parsedEndPoint.inheritanceModifierSpecified = true;
               parsedEndPoint.inheritanceModifier = (Parsing.inheritanceModifier)endPoint.InheritanceModifier;
            }

            parsedEndPoint.name = endPoint.Name;
        }
    }

}

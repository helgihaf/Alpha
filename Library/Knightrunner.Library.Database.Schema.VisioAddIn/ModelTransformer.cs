using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODELENGLib;
using System.IO;
using Knightrunner.Library.Database.Schema.Verification;
using System.ComponentModel;
using Knightrunner.Library.Database.Schema.SqlServer;
using Knightrunner.Library.Database.Schema.Linq;

namespace Knightrunner.Library.Database.Schema.VisioAddIn
{
    internal class ModelTransformer
    {
        private static VisioModelingEngine visioModelingEngine = new VisioModelingEngine();
        private const string TargetSystemNameForScript = "MSSQL";
        private const string TargetSystemNameForLinq = "DotNet";

        /// <summary>
        /// Returns the names of the models available, it is used by the user interface
        /// to let the user choose which data model to generate, in case more than one
        /// is loaded in memory
        /// </summary>
        /// <returns></returns>
        public static string[] GetModels()
        {
            List<string> result = new List<string>();

            IEnumIVMEModels modelsEnumerator = visioModelingEngine.Models;
            for (IVMEModel model = modelsEnumerator.Next(); model != null; model = modelsEnumerator.Next())
            {
                result.Add(model.Filename);
            }
            return result.ToArray();
        }


        private BackgroundWorker backgroundWorker;


        public string ModelFilePath { get; set; }
        public string DataTypesFilePath { get; set; }
        public string DataSchemaFilePath { get; set; }
        public string SqlScriptFilePath { get; set; }
        public string LinqToSqlFilePath { get; set; }


        internal void StartTransform(BackgroundWorker backgroundWorker)
        {
            this.backgroundWorker = backgroundWorker;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataSchema dataSchema = InitializeSchema();
            if (dataSchema == null || backgroundWorker.CancellationPending)
                return;

            ReadVisio(dataSchema);

            if (DataSchemaFilePath != null && !backgroundWorker.CancellationPending)
            {
                LogLine("Writing data schema file");
                dataSchema.Save(DataSchemaFilePath);
            }

            if (SqlScriptFilePath != null && !backgroundWorker.CancellationPending)
            {
                LogLine("Generating script file");
                GenerateScript(dataSchema);
            }

            if (LinqToSqlFilePath != null && !backgroundWorker.CancellationPending)
            {
                LogLine("Generating LINQ to SQL file");
                GenerateLinqToSql(dataSchema);
            }
        }


        private void LogLine(string line)
        {
            backgroundWorker.ReportProgress(0, line);
        }

        private DataSchema InitializeSchema()
        {
            var verificationContext = new VerificationContext();
            DataSchema dataSchema = new DataSchema();
            dataSchema.Name = Path.GetFileNameWithoutExtension(DataTypesFilePath);
            using (StreamReader reader = new StreamReader(DataTypesFilePath))
            {
                dataSchema.LoadDataSchemaFile(reader, verificationContext);
            }
            dataSchema.Verify(verificationContext);

            if (!backgroundWorker.CancellationPending && verificationContext.HasErrors)
            {
                ReportErrors(verificationContext);
                return null;
            }

            return dataSchema;
        }



        private void ReadVisio(DataSchema dataSchema)
        {
            // Search for the right model among the ones available in
            // the visioModelingEngine
            IEnumIVMEModels modelsEnumerator = visioModelingEngine.Models;
            IVMEModel model = modelsEnumerator.Next();
            while (model.Filename != ModelFilePath)
            {
                model = modelsEnumerator.Next();
            }

            if (backgroundWorker.CancellationPending)
                return;

            ReadTables(dataSchema, model.Elements);
            ReadRelations(dataSchema, model.Elements);
        }

        private void ReadTables(DataSchema dataSchema, IEnumIVMEModelElements elementEnumerator)
        {
            //  Iterate over all the entities in the model, searching
            //  for tables.
            //  Each entity is a table, we gather column and index information from here
            for (IVMEModelElement element = elementEnumerator.Next(); element != null; element = elementEnumerator.Next())
            {
                if (backgroundWorker.CancellationPending)
                    return;

                if (element.Type == VMEModelElementKind.eVMEKindEREntity)
                {
                    IVMEEntity entity = element as IVMEEntity;

                    Table newTable = new Table();
                    newTable.Name = entity.PhysicalName;
                    dataSchema.Tables.Add(newTable);

                    //
                    //  Here we search for the columns of the table
                    //
                    IEnumIVMEAttributes columnEnumerator = entity.Attributes;
                    for (IVMEAttribute columnDefinition = columnEnumerator.Next(); columnDefinition != null; columnDefinition = columnEnumerator.Next())
                    {
                        if (backgroundWorker.CancellationPending)
                            return;
                        Column newColumn = new Column();
                        newColumn.Name = columnDefinition.PhysicalName;
                        string columnTypeName = GetColumnTypeName(columnDefinition.DataType);
                        newColumn.ColumnType = FindColumnType(dataSchema, columnTypeName);
                        if (newColumn.ColumnType == null)
                        {
                            throw CreateException("Column type " + columnTypeName + " used in table " + newTable.Name + " was not found in the type definition document");
                        }

                        newColumn.CanBeNull = columnDefinition.AllowNulls;
                        newTable.Columns.Add(newColumn);
                    }

                    //
                    //  Now we search for indexes in the structure
                    //
                    IEnumIVMEEntityAnnotations indexesEnumerator = entity.EntityAnnotations;
                    for (IVMEEntityAnnotation indexDefinition = indexesEnumerator.Next(); indexDefinition != null; indexDefinition = indexesEnumerator.Next())
                    {
                        if (backgroundWorker.CancellationPending)
                            return;
                        bool isPrimaryKey = indexDefinition.kind == VMEEREntityAnnotationKind.eVMEEREntityAnnotationPrimary;
                        if (!isPrimaryKey)
                        {
                            // Primary keys in the Knightrunner DataSchema are not index objects
                            Index newIndex = new Index(newTable);
                            newIndex.Name = indexDefinition.PhysicalName;
                            newIndex.IsUnique = indexDefinition.kind == VMEEREntityAnnotationKind.eVMEEREntityAnnotationAlternate;

                            IEnumIVMEAttributes indexColumnEnumerator = indexDefinition.Attributes;
                            for (IVMEAttribute indexColumn = indexColumnEnumerator.Next(); indexColumn != null; indexColumn = indexColumnEnumerator.Next())
                            {
                                newIndex.Columns.Add(newTable.Columns[indexColumn.PhysicalName]);
                            }

                            newTable.Indices.Add(newIndex);
                        }
                        else
                        {
                            IEnumIVMEAttributes indexColumnEnumerator = indexDefinition.Attributes;
                            for (IVMEAttribute indexColumn = indexColumnEnumerator.Next(); indexColumn != null; indexColumn = indexColumnEnumerator.Next())
                            {
                                newTable.Columns[indexColumn.PhysicalName].InPrimaryKey = true;
                            }
                        }
                    }

                }
            }
        }

        private void ReadRelations(DataSchema dataSchema, IEnumIVMEModelElements elementEnumerator)
        {
            //
            //  Iterates over all the entities in the model, searching
            //  for relationships.
            //
            //  for each relationship, we need to find the related tables
            //  and all the interesting properties of the relationship
            //
            for (IVMEModelElement element = elementEnumerator.Next(); element != null; element = elementEnumerator.Next())
            {
                if (element.Type == VMEModelElementKind.eVMEKindERRelationship)
                {
                    IVMEBinaryRelationship relationship = element as IVMEBinaryRelationship;

                    if (relationship.FirstEntity != null && relationship.SecondEntity != null)
                    {
                        string relationName = relationship.PhysicalName;
                        Table primaryTable = dataSchema.Tables[relationship.FirstEntity.PhysicalName];
                        Table referencedTable = dataSchema.Tables[relationship.SecondEntity.PhysicalName];

                        //
                        //  Now we need to add the columns that are used to mantain the
                        //  relationship. We need two cursors, one for the primary
                        //  table and the other for the referenced one
                        //
                        IEnumIVMEAttributes primaryColumnEnumerator = relationship.FirstAttributes;
                        IVMEAttribute primaryColumn = primaryColumnEnumerator.Next();

                        IEnumIVMEAttributes referencedColumnEnumerator = relationship.SecondAttributes;
                        IVMEAttribute referencedColumn = referencedColumnEnumerator.Next();

                        while (primaryColumn != null)
                        {
                            Column primaryDataColumn = primaryTable.Columns[primaryColumn.PhysicalName];
                            Column referencedDataColumn = referencedTable.Columns[referencedColumn.PhysicalName];

                            ForeignKey foreignKey = new ForeignKey();
                            foreignKey.FromTable = primaryTable;
                            foreignKey.ToTable = referencedTable;
                            foreignKey.Columns.Add(new ForeignKey.ColumnPair { FromColumn = primaryDataColumn, ToColumn = referencedDataColumn });

                            primaryColumn = primaryColumnEnumerator.Next();
                            referencedColumn = referencedColumnEnumerator.Next();
                        }
                    }
                }
            }
        }


        private Exception CreateException(string msg)
        {
            return new ApplicationException(msg);
        }


        private string GetColumnTypeName(IVMEDataType dataType)
        {
            string result = null;
            if (dataType != null)
            {
                string physicalName = dataType.PhysicalName;
                if (physicalName != null)
                {
                    physicalName = physicalName.Trim();
                    result = physicalName.Split(' ')[0];
                }
            }

            return result;
        }


        private ColumnType FindColumnType(DataSchema dataSchema, string columnTypeName)
        {
            return dataSchema.ColumnTypes[columnTypeName];
        }

        
        private void ReportErrors(VerificationContext verificationContext)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in verificationContext.Entries)
            {
                sb.AppendLine(error.ToString());
            }
            backgroundWorker.ReportProgress(0, sb.ToString());
        }


        private void GenerateScript(DataSchema dataSchema)
        {
            TargetSystem targetSystem = dataSchema.TargetSystems[TargetSystemNameForScript];
            SqlServerScriptGenerator gen = new SqlServerScriptGenerator();
            gen.TargetSystem = targetSystem;
            gen.DataSchema = dataSchema;
            using (FileStream stream = new FileStream(SqlScriptFilePath, FileMode.Create))
            {
                gen.Generate(stream);
            }
        }

        private void GenerateLinqToSql(DataSchema dataSchema)
        {
            TargetSystem targetSystemLinq = dataSchema.TargetSystems[TargetSystemNameForLinq];
            TargetSystem targetSystemScript = dataSchema.TargetSystems[TargetSystemNameForScript];

            LinqDbmlGenerator linqGen = new LinqDbmlGenerator();
            linqGen.DataSchema = dataSchema;
            linqGen.TargetSystem = targetSystemLinq;
            linqGen.DatabaseTargetSystem = targetSystemScript;
            linqGen.TableSchemaName = "dbo";
            linqGen.DatabaseColumnTypeMapper = new SqlServerColumnTypeMapper();
            using (FileStream stream = new FileStream(LinqToSqlFilePath, FileMode.Create))
            {
                linqGen.Generate(stream);
            }
        }

    }
}

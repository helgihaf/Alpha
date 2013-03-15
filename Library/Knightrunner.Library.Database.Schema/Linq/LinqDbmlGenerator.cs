using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema.Linq
{
    public class LinqDbmlGenerator 
    {
        private const string xmlNamespace = "http://schemas.microsoft.com/linqtosql/dbml/2007";

        private IColumnTypeMapper linqColumnTypeMapper;

        public class ConnectionSettings
        {
            public string Mode { get; set; } 
            public string ConnectionString { get; set; } //="Data Source=(local);Initial Catalog=GameCore;Integrated Security=True" 
            public string SettingsObjectName { get; set; } //="TestMisc.Properties.Settings" 
            public string SettingsPropertyName { get; set; } //="GameCoreConnectionString" 
            public string Provider { get; set; } //="System.Data.SqlClient" />
        }

        public DataSchema DataSchema { get; set; }
        public string DatabaseSchemaName { get; set; }
        public TargetSystem TargetSystem { get; set; }
        public TargetSystem DatabaseTargetSystem { get; set; }
        public IColumnTypeMapper DatabaseColumnTypeMapper { get; set; }

        public string DirectoryPath { get; set; }
        public string FileName { get; set; }

        public ConnectionSettings Connection { get; set; }


        public LinqDbmlGenerator()
        {
            this.Connection = new ConnectionSettings
            {
                Mode = "AppSettings",
                Provider = "System.Data.SqlClient"
            };

            linqColumnTypeMapper = new LinqColumnTypeMapper();
        }


        public void Generate()
        {
            if (string.IsNullOrWhiteSpace(DatabaseSchemaName))
            {
                throw new InvalidOperationException("DatabaseSchemaName can not be empty");
            }
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

            if (TargetSystem == null)
                throw new InvalidOperationException("TargetSystem property has not been set");

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

            var document =
                new XDocument
                (
                    new XDeclaration("1.0", "utf-8", null),
                    //<Database Name="GameCore" Class="DatabaseDataContext" xmlns="http://schemas.microsoft.com/linqtosql/dbml/2007">
                    new XElement
                    (
                        XName.Get("Database", xmlNamespace),
                        new XAttribute("Name", DataSchema.Name),
                        new XAttribute("Class", DataSchema.Name + "DataContext"),
                        GenerateConnection(),
                        GenerateTables()
                    )
                );

            using (StreamWriter writer = new StreamWriter(outStream, Encoding.UTF8))
            {
                document.Save(writer);
            }
        }

        private XElement GenerateConnection()
        {
            var element = new XElement(XName.Get("Connection", xmlNamespace));
            if (Connection.Mode != null)
                element.Add(new XAttribute("Mode", Connection.Mode));
            if (Connection.ConnectionString != null)
                element.Add(new XAttribute("ConnectionString", Connection.ConnectionString));
            if (Connection.SettingsObjectName != null)
                element.Add(new XAttribute("SettingsObjectName", Connection.SettingsObjectName));
            if (Connection.SettingsPropertyName != null)
                element.Add(new XAttribute("SettingsPropertyName", Connection.SettingsPropertyName));
            if (Connection.Provider != null)
                element.Add(new XAttribute("Provider", Connection.Provider));

            return element;
        }

        private IEnumerable<XElement> GenerateTables()
        {
            // First collect all associations (foreign keys) between tables. They are then applied 
            // to the XML stream when processing the columns for a table.
            Associations associations = new Associations();
            foreach (Table table in DataSchema.Tables)
            {
                foreach (ForeignKey foreignKey in table.ForeignKeys)
                {
                    associations.Add(table, foreignKey);
                }
            }

            // Generate the XML for each table
            foreach (Table table in DataSchema.Tables)
            {
                yield return GenerateTable(table, associations);
            }
        }

        private XElement GenerateTable(Table table, Associations associations)
        {
            string fullTableName;
            if (DatabaseSchemaName != null)
                fullTableName = DatabaseSchemaName + "." + table.Name;
            else
                fullTableName = table.Name;

            var tableElement = new XElement(XName.Get("Table", xmlNamespace), new XAttribute("Name", fullTableName));

            tableElement.Add(new XAttribute("Member", NameHelper.GetMemberNamePlural(table)));

            var typeElement = new XElement(XName.Get("Type", xmlNamespace));
            string typeName = NameHelper.GetTypeName(table);
            typeElement.Add(new XAttribute("Name", typeName));
            
            // Columns
            foreach (Column column in table.Columns)
            {
                var columnElement = new XElement(XName.Get("Column", xmlNamespace));

                //Name="Id" Type="System.Int32" DbType="Int NOT NULL IDENTITY" IsPrimaryKey="true" IsDbGenerated="true" CanBeNull="false" />
                columnElement.Add(new XAttribute("Name", column.Name));
                columnElement.Add(new XAttribute("Type", linqColumnTypeMapper.GetColumnTypeString(TargetSystem, column)));
                columnElement.Add(new XAttribute("DbType", DatabaseColumnTypeMapper.GetColumnTypeString(DatabaseTargetSystem, column)));
                if (column.InPrimaryKey)
                    columnElement.Add(new XAttribute("IsPrimaryKey", "true"));
                if (column.ColumnType.IsDbGenerated)
                    columnElement.Add(new XAttribute("IsDbGenerated", "true"));
                columnElement.Add(new XAttribute("CanBeNull", column.CanBeNull ? "true" : "false"));
                
                // Handle extended properties, if any
                var target = column.GetEffectiveTarget(TargetSystem);
                if (target != null)
                {
                    for (int i = 0; i < target.ExtendedProperties.Count; i++)
                    {
                        columnElement.Add(new XAttribute(target.ExtendedProperties.GetKey(i), target.ExtendedProperties.Get(i)));
                    }
                }
   
                typeElement.Add(columnElement);
            }

            foreach (var association in associations.GetForToTable(table.Name))
            {
                typeElement.Add(association.CreateXElement(xmlNamespace));
            }

            foreach (var association in associations.GetForFromTable(table.Name))
            {
                typeElement.Add(association.CreateXElement(xmlNamespace));
            }

            tableElement.Add(typeElement);

            return tableElement;
        }


    }



}

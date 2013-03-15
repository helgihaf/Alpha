using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Versioning.SqlServer
{
    public class SqlServerXmlVersionStore : IDatabaseVersionStore
    {
        public const string DefaultTableName = "DatabaseVersion";
        public const string DefaultSchemaName = "dbo";

        private const string xmlColumnName = "Xml";

        private SqlConnection sqlConnection;
        private string schemaName;
        private string tableName;

        private bool cachingEnabled = true;
        private Version lastVersion;

        private Knightrunner.Library.Database.SqlServer.SqlServerDbServerFactory serverFactory = new Database.SqlServer.SqlServerDbServerFactory(DatabaseServerType.SqlServer);

        public SqlServerXmlVersionStore(SqlConnection sqlConnection)
            : this(sqlConnection, DefaultSchemaName, DefaultTableName)
        {
        }

        public SqlServerXmlVersionStore(SqlConnection sqlConnection, string schemaName)
            : this(sqlConnection, schemaName, DefaultTableName)

        {
        }

        public SqlServerXmlVersionStore
        (
            SqlConnection sqlConnection, 
            string schemaName, 
            string tableName
        )
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection");
            }
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException("schemaName");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            this.sqlConnection = sqlConnection;
            this.schemaName = schemaName;
            this.tableName = tableName;
        }


        public bool CachingEnabled
        {
            get { return cachingEnabled; }
            set { cachingEnabled = value; }
        }

        public void ClearCache()
        {
            lastVersion = null;
        }

        public Version Version
        {
            get
            {
                if (!CachingEnabled || lastVersion == null)
                {
                    lastVersion = GetVersionFromTable();
                }
                return lastVersion;
            }
            set
            {
                SetVersionInTable(value);
                lastVersion = value;
            }
        }

        private System.Version GetVersionFromTable()
        {
            if (!TableExists())
            {
                CreateTable();
                return null;
            }

            var xml = SelectXml(null);
            if (xml == null)
            {
                return null;
            }

            XDocument xdoc = XDocument.Parse(xml);
            var versionElement = xdoc.Root;
            if (versionElement == null)
            {
                throw new FormatException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.InvalidXmlVersionFormat, tableName));
            }

            return Version.Parse(versionElement.Value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private bool TableExists()
        {
            string commandText;
            string[] parameterNames;
            serverFactory.TableExistsCommandText(out commandText, out parameterNames);
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.Parameters.Add(new SqlParameter(parameterNames[0], this.tableName));
                if (parameterNames.Length == 2)
                {
                    cmd.Parameters.Add(new SqlParameter(parameterNames[1], this.schemaName));
                }

                return cmd.ExecuteScalar() != null;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private void CreateTable()
        {
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = string.Format(CultureInfo.InvariantCulture,
                    "CREATE TABLE [{0}].[{1}] ([Xml] Xml NOT NULL)", this.schemaName, this.tableName);
                cmd.ExecuteNonQuery();
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private string SelectXml(SqlTransaction transaction)
        {
            string cmdText = string.Format(CultureInfo.InvariantCulture, "SELECT [{0}] FROM [{1}].[{2}]", xmlColumnName, schemaName, tableName);

            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = cmdText;
                if (transaction != null)
                {
                    cmd.Transaction = transaction;
                }

                return cmd.ExecuteScalar() as string;
            }
        }



        private void SetVersionInTable(System.Version version)
        {
            using (var transaction = sqlConnection.BeginTransaction())
            {
                var xml = SelectXml(transaction);
                if (xml == null)
                {
                    var xdoc = new XDocument();
                    xdoc.Add(CreateVersionElement(version));
                    InsertXml(transaction, xdoc.ToString());
                }
                else
                {
                    var xdoc = XDocument.Parse(xml);
                    var versionElement = xdoc.Root;
                    if (versionElement != null)
                    {
                        versionElement.Remove();
                    }
                    xdoc.Add(CreateVersionElement(version));
                    UpdateXml(transaction, xml);
                }
                transaction.Commit();
            }
        }

        private XElement CreateVersionElement(System.Version version)
        {
            return new XElement("Version", version.ToString());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private void InsertXml(SqlTransaction transaction, string xml)
        {
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = string.Format(CultureInfo.InvariantCulture, "INSERT INTO [{0}].[{1}] ([{2}]) VALUES (@xmlValue)",
                    schemaName, tableName, xmlColumnName);
                if (transaction != null)
                {
                    cmd.Transaction = transaction;
                }
                cmd.Parameters.Add(new SqlParameter("@xmlValue", xml));
                cmd.ExecuteNonQuery();                
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private void UpdateXml(SqlTransaction transaction, string xml)
        {
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = string.Format(CultureInfo.InvariantCulture, "UPDATE [{0}].[{1}] SET [{2}] = @xmlValue", schemaName, tableName, xmlColumnName);
                if (transaction != null)
                {
                    cmd.Transaction = transaction;
                }
                cmd.Parameters.Add(new SqlParameter("@xmlValue", xml));
                cmd.ExecuteNonQuery();                
            }
        }
    }
}

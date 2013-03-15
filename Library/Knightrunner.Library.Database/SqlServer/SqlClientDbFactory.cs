using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace Knightrunner.Library.Database.SqlServer
{
    /// <summary>
    /// Implements a factory for SqlCleint database connections
    /// </summary>
    public class SqlClientDbFactory : DbFactoryBase
    {
        private IConnectionStringProvider connectionStringProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="providerFactory"></param>
        public SqlClientDbFactory(DbProviderFactory providerFactory) : base(providerFactory)
        {
            ServerFactory = new SqlServerDbServerFactory(DatabaseServerType.SqlServer);
        }

        /// <summary>
        /// Test the database connection
        /// </summary>
        /// <param name="theDbConnection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override bool TestConnection(IDbConnection theDbConnection, IDbTransaction transaction)
        {
            SqlConnection theSqlConnection = (SqlConnection)theDbConnection;
            bool result = true;
            const string sqlTest = "SELECT TOP 1 [id] FROM [sys].[sysobjects]";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlTest, (SqlConnection)theDbConnection, (SqlTransaction)transaction))
                {
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            result = false;
                    }
                }
            }
            catch(InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine("TestConnection exception: " + ex.Message);
                result = false;
            }
            catch(SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("TestConnection exception: " + ex.Message);
                result = false;
            }

            return result;
        }

        public override IDataParameter CreateParameter(string name, SqlDbType type)
        {
            return new SqlParameter(name, type);
        }


        /// <summary>
        /// Create a data parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override IDataParameter CreateParameter(string name, SqlDbType type, int size)
        {
            return new SqlParameter(name, type, size);
        }
        

        /// <summary>
        /// Returns a parameter placeholder.
        /// </summary>
        public override string ParameterSqlPlaceholder(string columnName)
        {
            return "@" + columnName;
        }



        public override IConnectionStringProvider ConnectionStringProvider
        {
            get
            {
                if (connectionStringProvider == null)
                {
                    connectionStringProvider = new SqlConnectionStringProvider();
                }
                return connectionStringProvider;
            }
        }
    }
}

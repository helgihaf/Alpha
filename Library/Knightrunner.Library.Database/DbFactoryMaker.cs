using System;
using System.Data;
using System.Data.Common;
using Knightrunner.Library.Database.SqlServer;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// Handles the creation of IDbFactory objects based on various criteria.
    /// </summary>
    public static class DbFactoryMaker
    {
        /// <summary>
        /// Create an IDbFactory based on provider invariant name.
        /// </summary>
        /// <param name="providerInvariantName">Provider invariant name, usually the namespace of
        /// the database classes requested, e.g. "System.Data.SqlClient"</param>
        /// <returns>An object implementing IDbFactory for the provider requested.</returns>
        public static IDbFactory CreateDbFactory(string providerInvariantName)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerInvariantName);
            if (providerFactory == null)
                throw new DataException("System.Data.Common.DbProviderFactories could not get a factory for provider invariant name " + providerInvariantName); 
            return CreateDbFactory(providerFactory);
        }

        /// <summary>
        /// Create an IDbFactory based on provider row.
        /// </summary>
        /// <param name="providerRow">Provider row data as fetched from DbProviderFactories.GetFactoryClasses()</param>
        /// <returns>An object implementing IDbFactory for the provider requested.</returns>
        public static IDbFactory CreateDbFactory(DataRow providerRow)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerRow);
            if (providerFactory == null)
                throw new DataException("System.Data.Common.DbProviderFactories could not get a factory for row.");
            return CreateDbFactory(providerFactory);
        }


        /// <summary>
        /// Create an IDbFactory based on a DbProviderFactory.
        /// </summary>
        /// <param name="providerFactory">A DbProviderFactory as returned from DbProviderFactories.GetFactory()</param>
        /// <returns>An object implementing IDbFactory for the provider requested.</returns>
        /// <exception cref="DatabaseException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDbFactory CreateDbFactory(DbProviderFactory providerFactory)
        {
            if (providerFactory == null)
                throw new ArgumentNullException("providerFactory");
                
            IDbFactory dbFactory;
            if (providerFactory is System.Data.SqlClient.SqlClientFactory)
                dbFactory = new SqlClientDbFactory(providerFactory as System.Data.SqlClient.SqlClientFactory);
            //else if (providerFactory is System.Data.Odbc.OdbcFactory)
            //    dbFactory = new DbFactoryOdbc(providerFactory as System.Data.Odbc.OdbcFactory);
            //else if (providerFactory is System.Data.OleDb.OleDbFactory)
            //    dbFactory = new DbFactoryOleDb(providerFactory as System.Data.OleDb.OleDbFactory);
            else
                throw new DataException("DbFactoryMaker has no support for DbProviderFactory of type " + providerFactory.GetType().FullName);
            
            return dbFactory;
        }

        public static IDbServerFactory CreateDbServerFactory(DatabaseServerType serverType)
        {
            switch (serverType)
            {
                case DatabaseServerType.Undefined:
                    throw new ArgumentException("Cannot create an IDbServerFactory for DatabaseServerType." + serverType.ToString());
                case DatabaseServerType.SqlServer:
                    return new SqlServerDbServerFactory(DatabaseServerType.SqlServer);
                //case DatabaseServerType.Informix:
                //    return new DbServerFactoryInformix(DatabaseServerType.Informix);
                default:
                    throw new NotImplementedException("Implementation missing for enumerated type DatabaseServerType." + serverType.ToString());
            }
        }
        
    }
}

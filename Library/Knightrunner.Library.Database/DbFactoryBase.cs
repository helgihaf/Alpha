using System;
using System.Data;
using System.Data.Common;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// DbFactoryBase is an abstract base class for classes that implement the IDbFactory interface.
    /// </summary>
    public abstract class DbFactoryBase : IDbFactory
    {
        private readonly DbProviderFactory providerFactory;
        private IDbServerFactory serverFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DbFactoryBase(DbProviderFactory providerFactory)
        {
            if (providerFactory == null)
                throw new ArgumentNullException("providerFactory");
            this.providerFactory = providerFactory;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IDbConnection CreateConnection()
        {
            return providerFactory.CreateConnection();
        }

        public IDbCommand CreateCommand()
        {
            return providerFactory.CreateCommand();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public IDbCommand CreateCommand(string cmdText)
        {
            IDbCommand cmd = providerFactory.CreateCommand();
            cmd.CommandText = cmdText;
            return cmd;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public IDbCommand CreateCommand(string cmdText, IDbConnection connection)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = cmdText;
            return cmd;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public IDbCommand CreateCommand(string cmdText, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.Transaction = transaction;
            return cmd;
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return providerFactory.CreateDataAdapter();
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            IDbDataAdapter adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            return adapter;
        }

        public abstract bool TestConnection(IDbConnection connection, IDbTransaction transaction);

        public IDataParameter CreateParameter(object val)
        {
            return providerFactory.CreateParameter();
        }

        public IDataParameter CreateParameter(string name, object val)
        {
            IDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = val;
            return parameter;
        }

        public abstract IDataParameter CreateParameter(string name, SqlDbType type);

        public abstract IDataParameter CreateParameter(string name, SqlDbType type, int size);


        /// <summary>
        /// ServerType property.
        /// </summary>
        public IDbServerFactory ServerFactory
        {
            get { return serverFactory; }
            set { serverFactory = value; }
        }

        public abstract string ParameterSqlPlaceholder(string parameterName);

        public virtual string SqlIdentifier(string identifier)
        {
            return ServerFactory.SqlIdentifier(identifier);
        }


        /// <summary>
        /// Gets a connection string provider.
        /// </summary>
        public abstract IConnectionStringProvider ConnectionStringProvider { get; }


        public string DisplayableConnectionString(string connectionString)
        {
            int startIndex = connectionString.IndexOf(ConnectionStringProvider.PasswordProperty, StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
            {
                // Use heuristcs
                startIndex = connectionString.IndexOf("pwd", StringComparison.OrdinalIgnoreCase);
                if (startIndex == -1)
                {
                    startIndex = connectionString.IndexOf("password", StringComparison.OrdinalIgnoreCase);
                }
            }
            
            if (startIndex == -1)
                return connectionString;
            
            // Strip password from string
            
            int passwordEquIndex = connectionString.IndexOf('=', startIndex);
            if (passwordEquIndex == -1)
            {
                // We have no "=" so we deduce that we have no password string
                return connectionString;
            }

            // Set results as the beginning of what we've got
            string result = connectionString.Substring(0, passwordEquIndex+1);
            
            // Append the rest of the connection string, following the password
            int passwordEndIndex = connectionString.IndexOf(';', passwordEquIndex);
            if (passwordEndIndex != -1)
            {
                result += connectionString.Substring(passwordEndIndex);
            }
            
            return result;
        }
    }
}

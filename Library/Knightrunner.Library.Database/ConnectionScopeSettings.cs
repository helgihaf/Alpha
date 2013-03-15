using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database
{
    public class ConnectionScopeSettings
    {
        private IDbFactory factory;
        private string connectionString;
        private int lockTimeoutMs = 10000;	// milliseconds
        private int commandTimeoutSeconds = 30;	// seconds
        private System.Transactions.IsolationLevel transactionIsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;


        /// <summary>
        /// Gets or sets the factory of the settings. The default is System.Data.SqlClient.
        /// </summary>
        public IDbFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    factory = DbFactoryMaker.CreateDbFactory("System.Data.SqlClient");
                }
                return factory;
            }

            set
            {
                factory = value;
            }
        }


        /// <summary>
        /// Gets or sets the connection string for new connections.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        /// <summary>
        /// Gets or sets the lock timeout in milliseconds used in connections. A value of
        /// -1 means no timeout (infinite).
        /// </summary>
        public int LockTimeoutMs
        {
            get
            {
                return lockTimeoutMs;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("Value for DefaultLockTimeoutMs must be -1 or greater");
                }
                lockTimeoutMs = value;
            }
        }

        /// <summary>
        /// Gets or sets the timeout in seconds all IDbCommand objects created in a
        /// ConnectionScope. A value of 0 means infinity.
        /// </summary>
        public int CommandTimeoutSeconds
        {
            get
            {
                return commandTimeoutSeconds;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value for DefaultCommandTimeoutSec must be 0 or greater");
                }
                commandTimeoutSeconds = value;
            }
        }

        /// <summary>
        /// Gets or sets the transaction isolation level used in transactions. Note that
        /// this setting does not propagate to a connection until a transaction scope is actually
        /// created using the ConnectionScope.CreateTransactionScope() method.
        /// </summary>
        public System.Transactions.IsolationLevel TransactionIsolationLevel
        {
            get
            {
                return transactionIsolationLevel;
            }
            set
            {
                transactionIsolationLevel = value;
            }
        }


    }
}

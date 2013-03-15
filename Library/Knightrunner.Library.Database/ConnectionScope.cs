using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.Diagnostics;
using System.Data.Common;
using System.Globalization;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// Provides a database connection to a code block.
    /// </summary>
    /// <remarks>
    /// <para>This class should always be constructed in a using() clause.</para>
    /// <para>An instance of this class provides a code block with a database connection and a
    /// database factory (IDbFactory). It has the following features:
    /// <list type="bullet">
    /// <item><description>Each thread has it's own connection.</description></item>
    /// <item><description>Once a thread has caused a database connection to be opened, that
    /// connection is available to all code executing the the scope of the code block that
    /// created the <c>ConnectionScope</c> instance, including other member functions called
    /// from inside the scope.</description></item>
    /// <item><description>Once a threads top level scope finishes, the previously opened connection
    /// is closed, thus returning it to the default database connection pool.</description></item>
    /// <item><description>The connections used by the <c>ConnectionScope</c> instance
    /// automatically paricipate in a transaction started through a <c>TransactionScope</c>,
    /// provided that the <c>TransactionScope</c> instance was created before the
    /// <c>ConnectionScope</c>.</description></item>
    /// <item><description>Connection attributes such as connection string, client type and
    /// server type, can be set to default values which all connections will use by default.
    /// These attributes can also be set in the <c>ConnectionScope</c> constructor.</description></item>
    /// <item><description>A <c>ConnectionScope</c> can be created and forced to use
    /// a new connection, an existing connection or the default create-as-needed behaviour.</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    public sealed class ConnectionScope : IDisposable
    {
        /// <summary>
        /// Private class used to store any new combination of factory + connection for a thread.
        /// </summary>
        private class Context
        {
            public IDbFactory Factory;
            public IDbConnection Connection;
            public int ReferenceCount;			// used to signal closing of Connection.
            public Transaction Transaction;		// used to keep track of transaction participation.
            public int LockTimeoutMs;			// last set lock timeout, used to check if we need
                                                // to restore lock timeout on ConnectionScope.Dispose().

            public Context(IDbFactory factory, string connectionString)
            {
                this.Factory = factory;
                this.Connection = Factory.CreateConnection();
                this.Connection.ConnectionString = connectionString;
            }
        }

        //
        // Static part
        //
        private static ConnectionScopeSettings defaultSettings = new ConnectionScopeSettings();
        
        public static ConnectionScopeSettings DefaultSettings
        {
            get { return defaultSettings; }
            set { defaultSettings = value; }
        }

        private static object staticMonitor = new object();

        [ThreadStatic]
        private static Stack<Context> stack;

        /// <summary>
        /// Creates a TransactionScope object with the default options and isolation level.  
        /// This function does not need an active ConnectionScope block.
        /// </summary>
        /// <returns>A TransactionScope object with the default options and isolation level.</returns>
        public static TransactionScope CreateTransactionScope()
        {
            TransactionScope transactionScope;

            if (Transaction.Current == null)
            {
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = DefaultSettings.TransactionIsolationLevel;
                transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            }
            else
            {
                // An existing transaction is already in scope. Make sure we create a compatible
                // TransactionScope, otherwise call will fail with an exception
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = Transaction.Current.IsolationLevel;
                transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            }

            return transactionScope;
        }

        /// <summary>
        /// Creates a TransactionScope object with the default isolation level and the specified
        /// option. This function does not need an active ConnectionScope block.
        /// </summary>
        /// <returns>A TransactionScope object with the default isolation level and the specified
        /// option.</returns>
        public static TransactionScope CreateTransactionScope(TransactionScopeOption option)
        {
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = DefaultSettings.TransactionIsolationLevel;
            return new TransactionScope(option, transactionOptions);
        }


        /// <summary>
        /// Use this to check if there is an open connection ready for the curren thread. This
        /// function exists primarily for status checking, it should NOT be used for decisions on
        /// wether to open a new connection or not.
        /// </summary>
        /// <returns>True if a connection is open for the current thread, false otherwise.</returns>
        public static bool IsConnectionOpen()
        {
            return stack.Count > 0;
        }

        /// <summary>
        /// Checks is the current transaction for the current thread has been aborted, i.e. will be
        /// rolled back. NOTE: If no transaction exists for the current thread, the function returns
        /// true.
        /// </summary>
        /// <returns>True if the current transaction is aborted or there is no transaction, false
        /// otherwise.</returns>
        public static bool IsTransactionAborted()
        {
            return Transaction.Current == null || Transaction.Current.TransactionInformation.Status == TransactionStatus.Aborted;
        }

        /// <summary>
        /// Returns the active factory for the current thread.
        /// </summary>
        /// <remarks>
        /// The active factory is the factory of the current open connection in the current thread,
        /// or, if no connection exists, the default factory.
        /// </remarks>
        public static IDbFactory ActiveFactory
        {
            get
            {
                if (stack != null && stack.Count > 0)
                    return stack.Peek().Factory;
                else
                    return DefaultSettings.Factory;
            }
        }

        /// <summary>
        /// Returns true if there exists a connection scope in this thread, false otherwise.
        /// </summary>
        public static bool HasCurrent
        {
            get
            {
                return stack != null && stack.Count > 0;
            }
        }

        //
        // Instance part
        //
        private Context context;
        private bool disposed;
        private ConnectionScopeOptions options;
        private int previousLockTimeoutMs;

        /// <summary>
        /// Initializes a new instance of the ConnectionScope class. 
        /// </summary>
        /// <example>
        /// <code>
        /// using(ConnectionScope cs = new ConnectionScope())
        /// {
        ///		using(IDbCommand cmd = cs.Factory.CreateCommand())
        ///		{
        ///			cmd.CommandText = "insert into foo (bar) values(42)";
        ///			cmd.ExecuteNonQuery();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        /// Initializes a new instance of the ConnectionScope class with the Required option and
        /// the default factory and connection string.
        /// </remarks>
        public ConnectionScope()
            : this(ConnectionScopeOptions.Required)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionScope class with the specified option.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <remarks>
        /// Initializes a new instance of the ConnectionScope class with the specified option and
        /// the default factory and connection string.
        /// If a TransactionScope is in effect, the connection will participate in that transaction.
        /// </remarks>
        public ConnectionScope(ConnectionScopeOptions options)
        {
            if (string.IsNullOrEmpty(DefaultSettings.ConnectionString))
            {
                throw new InvalidOperationException("ConnectionString property has not been initialized");
            }
            Initialize(DefaultSettings.Factory, DefaultSettings.ConnectionString, options);
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionScope class with the specified connection
        /// string.
        /// </summary>
        /// <param name="connectionString">The connection string to use.</param>
        /// <remarks>
        /// Initializes a new instance of the ConnectionScope class with the specified connection
        /// string, the Required option and the default factory.
        /// If a TransactionScope is in effect, the connection will participate in that transaction.
        /// </remarks>
        public ConnectionScope(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            Initialize(DefaultSettings.Factory, connectionString, ConnectionScopeOptions.Required);
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionScope class with the specified factory and
        /// connection string.
        /// </summary>
        /// <param name="factory">The factory to use.</param>
        /// <param name="connectionString">The connection string to use.</param>
        /// <remarks>
        /// Initializes a new instance of the ConnectionScope class with the specified factory and
        /// connection string and the Required option.
        /// If a TransactionScope is in effect, the connection will participate in that transaction.
        /// </remarks>
        public ConnectionScope(IDbFactory factory, string connectionString)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            Initialize(factory, connectionString, ConnectionScopeOptions.Required);
        }

        /// <summary>
        /// Initializer used only by constructors.
        /// </summary>
        private void Initialize(IDbFactory argFactory, string argConnectionString, ConnectionScopeOptions options)
        {
            Debug.Assert(!string.IsNullOrEmpty(argConnectionString));
            Debug.Assert(argFactory != null);

            this.options = options;

            if (stack == null)
            {
                stack = new Stack<Context>();
            }

            if (options == ConnectionScopeOptions.RequiresExisting && stack.Count == 0)
            {
                throw new InvalidOperationException("No connection exists for a ConnectionScope with option RequiresExisting");
            }

            if (options != ConnectionScopeOptions.RequiresNew && CanReuseExistingConnection(argFactory, argConnectionString))
            {
                // We can reuse an existing connection
                this.context = stack.Peek();
                this.context.ReferenceCount++;
                // Make sure that the connection is open
                AssertOpen(this.context.Connection);

                this.previousLockTimeoutMs = this.context.LockTimeoutMs;
                // Has a Transaction been started since the existing connection was created?
                if (this.context.Transaction == null && Transaction.Current != null)
                {
                    // Enlist the existing connection in the transaction.
                    // This must be done because the connection is older than the transaction.
                    ((DbConnection)this.context.Connection).EnlistTransaction(Transaction.Current);
                    this.context.Transaction = Transaction.Current;
                    // Instruct current transaction to notify us when the transaction completes
                    // (either sucessfully or unsuccessfully). This must be done so we don't enlist
                    // the next ConnectionScope in a transaction that has already been completed.
                    // Note that the event handler method is static.
                    Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler(CurrentTransactionCompleted);
                }
            }
            else
            {
                // We must open a new connection
                this.context = new Context(argFactory, argConnectionString);
                this.context.Connection.Open();
                SetLockTimeout(DefaultSettings.LockTimeoutMs);
                this.context.ReferenceCount++;
                this.previousLockTimeoutMs = DefaultSettings.LockTimeoutMs;
                if (Transaction.Current != null)
                {
                    this.context.Transaction = Transaction.Current;
                    Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler(CurrentTransactionCompleted);

                }
                stack.Push(context);
            }
        }

        /// <summary>
        /// Determines if the parameters specified match the connection at the top of the stack.
        /// </summary>
        private bool CanReuseExistingConnection(IDbFactory argFactory, string argConnectionString)
        {
            bool result = false;
            if (stack.Count > 0)
            {
                var context = stack.Peek();

                result =
                    context.Factory == argFactory &&
                    AreConnectionStringsEqual(context.Connection.ConnectionString, argConnectionString);
            }

            return result;
        }

        /// <summary>
        /// Determines if two connnection strings are equal, i.e. that they will yield the same
        /// kind of database connection.
        /// </summary>
        private bool AreConnectionStringsEqual(string cs1, string cs2)
        {
            // A connection string taken from an open SqlConnection object will not return
            // the Password property. Thus, we compare the connection strings without the
            // Password property.
            string value1 = ClipPassword(cs1);
            string value2 = ClipPassword(cs2);
            return StringComparer.InvariantCultureIgnoreCase.Compare(value1, value2) == 0;
        }

        /// <summary>
        /// Strips the password from a connection string.
        /// </summary>
        private string ClipPassword(string cs)
        {
            int index = cs.IndexOf("password", StringComparison.OrdinalIgnoreCase);
            if (index == -1)
                return cs;

            string result = cs.Substring(0, index);

            index = cs.IndexOf(";", index, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
                result += cs.Substring(index + 1);

            return result;
        }


        /// <summary>
        /// Make sure that the specified connection is open. If it not open then it will be
        /// opened.
        /// </summary>
        private static void AssertOpen(IDbConnection connection)
        {
            ConnectionState state = connection.State;
            if (state == ConnectionState.Closed || state == ConnectionState.Broken)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Event handler for transaaction completed event.
        /// </summary>
        private static void CurrentTransactionCompleted(object sender, TransactionEventArgs e)
        {
            if (stack != null)
            {
                foreach (Context context in stack)
                {
                    if (context.Transaction == e.Transaction)
                        context.Transaction = null;
                }
            }
        }

        /// <summary>
        /// Disposes of the <c>ConnectionScope</c>. If a new connection was opened for this scope,
        /// it is closed. If a operation progress was requested, it is disposed.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                // NOTE: We don't have any unmanaged resources so we really don't need the disposing
                // flag. We simply use it for similarity to the component cleanup pattern.

                // Check if we need to restore to previous value of lock timeout
                if (this.context.LockTimeoutMs != this.previousLockTimeoutMs)
                {
                    SetLockTimeout(this.previousLockTimeoutMs);
                }

                this.context.ReferenceCount--;

                if (this.context.ReferenceCount == 0)
                {
                    // No connection scope referencing the context anymore, we can pop it of the stack
                    // and close/dispose the database connection.
                    if (stack != null && stack.Count > 0)
                    {
                        Context stackTopContext = stack.Pop();
                        Debug.Assert(stackTopContext == this.context, "Context being disposed is not at top of stack - a using() or finally clause missing somewhere");
                    }
                    else
                    {
                        Debug.Assert(false, "ConnectionScope being disposed with closeOnDispose == true but stack is empty");
                    }
                    this.context.Connection.Dispose();
                    this.context.Connection = null;
                }
            }

            this.disposed = true;
        }


        /// <summary>
        /// Creates an IDbCommand object for the current connection. 
        /// </summary>
        /// <returns>An IDbCommand object for the current connection.</returns>
        public IDbCommand CreateCommand()
        {
            IDbCommand cmd = Connection.CreateCommand();
            cmd.CommandTimeout = DefaultSettings.CommandTimeoutSeconds;

            AssertOpen(Connection);

            return cmd;
        }


        /// <summary>
        /// Creates an IDbCommand object for the current connection with the supplied command text.
        /// </summary>
        /// <returns>An IDbCommand object for the current connection.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public IDbCommand CreateCommand(string cmdText)
        {
            IDbCommand cmd = CreateCommand();
            cmd.CommandText = cmdText;

            return cmd;
        }


        /// <summary>
        /// Returns the latest serial/identity value that was automatically created by the database
        /// server for the last insert statement in the current connection.
        /// </summary>
        /// <returns>The latest serial/identity value created by the database in the current connection.</returns>
        /// <exception cref="System.InvalidOperationException">No serial value was available</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Marel.Common.Exceptions.DatabaseException.#ctor(System.String)")]
        public int GetLastSerial()
        {
            int serial = -1;

            using (IDbCommand cmd = CreateCommand(Factory.ServerFactory.SerialCommandText()))
            {
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        serial = Convert.ToInt32(reader[0], CultureInfo.InvariantCulture);
                    }
                    else
                        throw new InvalidOperationException("Last-serial query did not return a value");
                }
            }

            return serial;
        }


        /// <summary>
        /// Gets the factory used by this connection scope.
        /// </summary>
        public IDbFactory Factory
        {
            get { return context.Factory; }
        }

        /// <summary>
        /// Gets the connection used by this connection scope.
        /// </summary>
        public IDbConnection Connection
        {
            get { return context.Connection; }
        }

        /// <summary>
        /// Gets the options used by this connection scope.
        /// </summary>
        public ConnectionScopeOptions Options
        {
            get { return options; }
        }

        /// <summary>
        /// Sets the lock timeout in milliseconds for the connection scope to the specified
        /// value. Use -1 to specify infinite wait for locks. When the connection scope is
        /// disposed, the previous lock timeout value is restored.
        /// </summary>
        /// <param name="lockTimeoutMs">Lock timeout in milliseconds or -1 for infinite wait.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public void SetLockTimeout(int lockTimeoutMs)
        {
            if (lockTimeoutMs < -1)
                throw new ArgumentException("Lock timeout must be -1 or greater");

            string cmdText = Factory.ServerFactory.SetLockTimeoutCommandText(lockTimeoutMs);
            using (IDbCommand cmd = CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
            }

            this.context.LockTimeoutMs = lockTimeoutMs;
        }



    }
}

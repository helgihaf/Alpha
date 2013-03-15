using System;
using System.Collections.Generic;
using System.Text;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// The IDbServerFactory interface defines all primitives needed for database server specific
    /// code.
    /// </summary>
    public interface IDbServerFactory
    {
        /// <summary>
        /// Returns the DatabaseServerType of this ServerFactory. There is a one-to-one mapping between
        /// a DatabaseServerType and a specific implementation of IDbServerFactory.
        /// </summary>
        DatabaseServerType ServerType { get; }
        
        /// <summary>
        /// Converts a string to a proper SQL identifier, suitable for the database server associcated
        /// with this factory.
        /// </summary>
        /// <example>
        /// For SQL Server the following assignement
        /// <code>
        /// Console.WriteLine( "select " + myFactory.SqlIdentifier("order") + " from salesOrders");
        /// </code>
        /// will show the following text on the console:
        /// <code>
        /// select [order] from salesOrders
        /// </code>
        /// </example>
        /// <param name="identifier">A string identifier</param>
        /// <returns>A properly formatted SQL identifier</returns>
        string SqlIdentifier(string identifier);

        /// <summary>
        /// Returns a command text for an IDbCommand to get the latest serial/identity value that was
        /// automatically created by the database server for the last insert statement. A command using
        /// this text should be executed via the ExecuteReader() method and the result should be a single
        /// row with a single column whose value can be converted without loss to a System.Int32 value.
        /// </summary>
        /// <returns>A command text for an IDbCommand to get the latest serial/identity value that was
        /// automatically created by the database server for the last insert statement.</returns>
        string SerialCommandText();

        /// <summary>
        /// Returns the default provider invariant name for this server type. The provider invariant name
        /// is used to create objects of type IDbFactory.
        /// </summary>
        string DefaultProviderInvariantName { get; }

        /// <summary>
        /// Returns true if the server supports connection using the provider invariant name supplied.
        /// </summary>
        /// <param name="provider">Provider invariant name</param>
        /// <returns>True if server supports provider, false otherwise.</returns>
        bool SupportsProvider(string provider);

        /// <summary>
        /// Returns an SQL representation of a simple value.
        /// </summary>
        /// <param name="value">The value to be represented as SQL</param>
        /// <returns>An SQL expression for the value.</returns>
        /// <remarks>
        /// The function accepts integer types, floating point types, decimal types, strings and dates.
        /// </remarks>
        /// <example>
        /// <para>
        /// <code>sql = "select * from mytable where name = " + ValueToSql("hello");</code>
        /// => select * from mytable where name = 'hello'
        /// </para>
        /// <para><code>sql = "select * from mytable where amount > " + ValueToSql(1.745);</code>
        /// => select * from mytable where amount > 1.745
        /// </para>
        /// </example>
        string ValueToSql(object value);

        /// <summary>
        /// Returns a command text for an IDbCommand to set the current lock timeout value for the
        /// current connection.
        /// </summary>
        /// <param name="timeoutMs">A timeout value in milliseconds. A value of -1 means infinite
        /// timeout.</param>
        /// <remarks>
        /// A command using this text should use the ExecuteNoQuery() method to set the lock
        /// timeout for the current connection.
        /// </remarks>
        /// <returns>A command text for an IDbCommand to set the current lock timeout value for the
        /// current connection.</returns>
        string SetLockTimeoutCommandText(int timeoutMs);

        /// <summary>
        /// Return a human readable text describing the error without all the garbage that is 
        /// contained in the standard error messages from the database.  Returns null if a 
        /// translation for the given error code is not found.
        /// </summary>
        /// <param name="errorCode">The error code to translate into human readable form</param>
        /// <returns>A human readable text describing the error without all the garbage that is 
        /// contained in the standard error messages from the database.  Returns null if a 
        /// translation for the given error code is not found.</returns>
        string TranslateErrorCode(int errorCode);

        /// <summary>
        /// Proviedes a command text for an IDbCommand to check for the existance of a table.
        /// The first parameter in the command text is the table name. Some implementation may need
        /// a second parameter for the schema name.
        /// </summary>
        /// <param name="commandText">A command text for an IDbCommand to check for the existance of a table.</param>
        /// <param name="parameterNames">The name of the parameter or parameters expected by the command text.
        /// Parameter 0 is for the table name; if parameter 1 exists, it is for the schema name.</param>
        void TableExistsCommandText(out string commandText, out string[] parameterNames);
    }
}

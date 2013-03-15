using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// The IDbFactory interface defines all primitives needed to create database client type
    /// specific objects.
    /// </summary>
    /// <remarks>
    /// For example, a CreateCommand() will return an OdbcCommand object for an
    /// ODBC client type, but an OleDbCommand for an OLE DB client type.
    /// </remarks>
    public interface IDbFactory
    {
        /// <summary>
        /// Creates an uninitialized IDbConnection.
        /// </summary>
        IDbConnection CreateConnection();


        /// <summary>
        /// Creates an uninitialized IDbCommand.
        /// </summary>
        IDbCommand CreateCommand();


        /// <summary>
        /// Creates an IDbCommand with the CommandText property set.
        /// </summary>
        IDbCommand CreateCommand(string cmdText);


        /// <summary>
        /// Creates an IDbCommand with the Connection and CommandText properties set.
        /// </summary>
        IDbCommand CreateCommand(string cmdText, IDbConnection connection);


        /// <summary>
        /// Creates an IDbCommand with the Connection, Transaction and CommandText properties set.
        /// </summary>
        IDbCommand CreateCommand(string cmdText, IDbConnection connection, IDbTransaction transaction);


        /// <summary>
        /// Creates an uninitialized IDbDataAdapter.
        /// </summary>
        IDbDataAdapter CreateDataAdapter();


        /// <summary>
        /// Creates an IDbDataAdapter for the specified command.
        /// </summary>
        IDbDataAdapter CreateDataAdapter(IDbCommand command);


        /// <summary>
        /// Tests a connection to see if it is still valid.
        /// </summary>
        /// <param name="connection">The connection to test.</param>
        /// <param name="transaction">The active transaction or null if no transaction is active.</param>
        /// <returns>True if the connection is valid, false otherwise.</returns>
        bool TestConnection(IDbConnection connection, IDbTransaction transaction);


        /// <summary>
        /// Creates an unnamed IDataParameter for an object value.
        /// </summary>
        /// <param name="val">The value of the parameter.</param>
        IDataParameter CreateParameter(object val);


        /// <summary>
        /// Creates a named IDataParameter for an object value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="val">The value of the parameter.</param>
        IDataParameter CreateParameter(string name, object val);


        /// <summary>
        /// Creates a named IDataParameter of a specific type. The parameter has no object value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter. It will be converted to the client's native type.</param>
        IDataParameter CreateParameter(string name, SqlDbType type);


        /// <summary>
        /// Creates a named IDataParameter of a specific type and a specific size. The parameter has no object value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter. It will be converted to the client's native type.</param>
        /// <param name="size">The size of the parameter.</param>
        IDataParameter CreateParameter(string name, SqlDbType type, int size);


        /// <summary>
        /// Gets or sets the database server factory associated with this factory. Classes implementing
        /// this property can set this to an appropriate default value.
        /// </summary>
        IDbServerFactory ServerFactory { get; set; }

        /// <summary>
        /// Returns an appropriate parameter placeholder for the client type to be used in SQL statements.
        /// </summary>
        /// <remarks>
        /// This is used when you want to use parameters in your SQL statements. For example, a valid
        /// SELECT statement for an ODBC client type using parameter placeholders could look like this:
        /// <code>SELECT id, name TFrom customers WHERE customerType = ?</code>
        /// while the same SELECT statement for an SqlServer client type would look like this:
        /// <code>SELECT id, name TFrom customers WHERE customerType = @customerType</code>
        /// </remarks>
        /// <param name="parameterName">The parameter name for which the placeholder will be constructed.</param>
        /// <returns>The parameter placeholder.</returns>
        string ParameterSqlPlaceholder(string parameterName);

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
        /// select "order" from salesOrders
        /// </code>
        /// </example>
        /// <param name="identifier">A string identifier</param>
        /// <returns>A properly formatted SQL identifier</returns>
        string SqlIdentifier(string identifier);

        /// <summary>
        /// Gets a connection string provider.
        /// </summary>
        IConnectionStringProvider ConnectionStringProvider { get; }

        /// <summary>
        /// Returns a connection string that can safely be displayed without revealing the database
        /// password.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>A displayable connection string.</returns>
        string DisplayableConnectionString(string connectionString);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Linq;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// The IDbContext interface provides a context binding the three most critical things used
    /// when developing a database application: Connection, transaction and factory.
    /// </summary>
    /// <remarks>
    /// Use this interface as parameter/property for code that has to be reusable and database
    /// independant. Instead of having three (or more) parameters to every reusable database
    /// function, you simply use this interface.
    /// </remarks>
    public interface IDbContext
    {    
        /// <summary>
        /// Gets the factory of this context.
        /// </summary>
        /// <value>The factory.</value>
        IDbFactory Factory { get; }
        
        /// <summary>
        /// Gets the connection of this context.
        /// </summary>
        /// <value>The connection.</value>
        IDbConnection Connection { get; }
        
        /// <summary>
        /// Gets the transaction of this context, or null if no transaction.
        /// </summary>
        /// <value>The transaction.</value>
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// Creates a new command using the factory, connection and transaction (if any) of the context.
        /// </summary>
        /// <param name="cmdText">The command text.</param>
        /// <returns>The new command</returns>
        IDbCommand CreateCommand(string cmdText);

        /// <summary>
        /// Gets the LINQ data context of this IDbContext.
        /// </summary>
        DataContext DataContext { get; }
    }
}

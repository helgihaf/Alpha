using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database
{
    /// <summary>
    /// Options for ConnectionScope constructors.
    /// </summary>
    public enum ConnectionScopeOptions
    {
        /// <summary>
        /// The default: Requires a connection within the scope. An existing connection will be
        /// used if any, otherwise a new connection will be created.
        /// </summary>
        Required,

        /// <summary>
        /// Requires that a new connection be created.
        /// </summary>
        /// <remarks>
        /// Use this option when you need to be independant of other connections in your thread,
        /// for example if running a transaction with TransactionScopeOptions.RequireNew.
        /// </remarks>
        RequiresNew,

        /// <summary>
        /// Requires that an existing connection be used. If no existing connection is found
        /// an exeption will be thrown from the constructor.
        /// </summary>
        /// <remarks>
        /// Use this option when your method returns a database object that is dependant on the
        /// existing connection for the thread.
        /// </remarks>
        RequiresExisting
    }

}

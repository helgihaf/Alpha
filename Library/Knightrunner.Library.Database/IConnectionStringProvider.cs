using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database
{
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Returns the connection string property name for specifying a database name.
        /// </summary>
        string DatabaseProperty { get; }

        /// <summary>
        /// Returns the connection string property name for specifying a user name.
        /// </summary>
        string UserNameProperty { get; }

        /// <summary>
        /// Returns the connection string property name for specifying a password.
        /// </summary>
        string PasswordProperty { get; }

        /// <summary>
        /// Returns the connection string property name for specifying integrated security. Integrated
        /// security is the ability of a database server to integrate it's login process with the one
        /// of the OS running the client. If a provider for a given ServerFactory does not support
        /// integrated security, this property will be blank (String.Empty).
        /// </summary>
        string IntegratedSecurityProperty { get; }
    }
}

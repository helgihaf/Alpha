using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.SqlServer
{
    public class SqlConnectionStringProvider : IConnectionStringProvider
    {
        #region IConnectionStringProvider Members

        public string DatabaseProperty
        {
            get { return "Initial Catalog"; }
        }

        public string UserNameProperty
        {
            get { return "User Id"; }
        }

        public string PasswordProperty
        {
            get { return "Password"; }
        }

        public string IntegratedSecurityProperty
        {
            get { return "Integrated Security"; }
        }


        #endregion
    }
}

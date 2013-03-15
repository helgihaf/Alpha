using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Knightrunner.Library.Database
{
    public abstract class DbContextBase : IDbContext
    {
        #region IDbContext Members

        public virtual IDbFactory Factory { get; set; }

        public virtual System.Data.IDbConnection Connection { get; set; }

        public virtual System.Data.IDbTransaction Transaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public virtual System.Data.IDbCommand CreateCommand(string cmdText)
        {
            IDbCommand cmd;
            if (Connection != null)
            {
                cmd = Connection.CreateCommand();
                cmd.CommandText = cmdText;
            }
            else
            {
                cmd = Factory.CreateCommand(cmdText);
            }

            return cmd;
        }

        public virtual System.Data.Linq.DataContext DataContext { get; set; }

        #endregion
    }
}

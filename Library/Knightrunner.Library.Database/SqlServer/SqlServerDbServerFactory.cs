//-------------------------------------------------------------------------------------------------
//
// DbServerFactorySqlServer.cs -- The DbServerFactorySqlServer class.
//
// Copyright (c) Marel hf. 2005. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;

namespace Knightrunner.Library.Database.SqlServer
{
    /// <summary>
    /// The DbServerFactorySqlServer class.
    /// </summary>
    public class SqlServerDbServerFactory : IDbServerFactory
    {

        public SqlServerDbServerFactory(DatabaseServerType serverType)
        {
            if (serverType != this.ServerType)
                throw new ArgumentException("ServerType" + serverType.ToString() + " is wrong for this class.", "serverType");
        }
        
        
        public DatabaseServerType ServerType
        {
            get { return DatabaseServerType.SqlServer; }
        }
        
        
        public string SqlIdentifier(string identifier)
        {
            return "[" + identifier.ToUpperInvariant() + "]";
        }

        public string SerialCommandText()
        {
            return "SELECT @@IDENTITY";
        }

        public string DefaultProviderInvariantName
        {
            get { return "System.Data.SqlClient"; }
        }

        public bool SupportsProvider(string provider)
        {
            switch (provider)
            {
                case "System.Data.SqlClient":
                    return true;
                //case "System.Data.OleDb":
                //    return true;
                //case "System.Data.Odbc":
                //    return true;
                default:
                    return false;
            }
        }

        public string ValueToSql(object value)
        {
            string sql;

            if (value == null)
            {
                sql = "NULL";
            }
            else if (value is DateTime)
            {
                DateTime dateTimeValue = (DateTime)value;
                sql = "'" + dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "'";
            }
            else if (value is bool)
            {
                sql = ((bool)value) ? "1" : "0";
            }
            else if (value is string)
            {
                sql = "'" + (string)value + "'";
            }
            else
            {
                CultureInfo ci = Thread.CurrentThread.CurrentCulture;
                try
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    sql = value.ToString();
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = ci;
                }
            }
            
            return sql;
        }


        public string SetLockTimeoutCommandText(int timeoutMs)
        {
            return "SET LOCK_TIMEOUT " + timeoutMs.ToString(CultureInfo.InvariantCulture);
        }

        public string TranslateErrorCode(int errorCode)
        {
            switch (errorCode)
            {
                case 547:
                    return "Cannot delete the row because it is being used by a different table.";
                case 2601:
                    return "Cannot insert duplicate key values.";
            }

            return null;
        }


        public void TableExistsCommandText(out string commandText, out string[] parameterNames)
        {
            parameterNames = new string[] { "@tableName",  "@schema" };
            commandText = string.Format(CultureInfo.InvariantCulture,
                "SELECT 1 FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_SCHEMA] = {0} AND [TABLE_NAME] = {1}",
                parameterNames[1], parameterNames[0]);
            
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Knightrunner.Library.Database.SqlServer
{
	public class SqlServerDbContext : IDbContext
	{
		private SqlClientDbFactory clientFactory;
		private IDbConnection connection;

		public SqlServerDbContext
		#region IDbContext Members

		public IDbFactory Factory
		{
			get
			{
				if (clientFactory == null)
				{
					clientFactory = new SqlClientDbFactory(System.Data.Common.DbProviderFactories.GetFactory(SqlServerDbServerFactory.DefaultProviderInvariantName));
				}
				return clientFactory;
			}
		}

		public System.Data.IDbConnection Connection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public System.Data.IDbTransaction Transaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public System.Data.IDbCommand CreateCommand(string cmdText)
		{
			throw new NotImplementedException();
		}

		public System.Data.Linq.DataContext DataContext
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}

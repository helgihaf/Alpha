using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smo = Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace Knightrunner.Library.Database.Schema.SqlServer
{
    public class SqlServerReverseEngineer
    {
        //public void ReverseEngineer(string connectionString)
        //{
        //    //ServerConnection serverConnection = new ServerConnection(
        //    //Server server = new Server(serverConnection);

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        ServerConnection serverConnection = new ServerConnection(connection);
        //        Console.WriteLine("DatabaseName = " + serverConnection.DatabaseName);

        //        DataSchema dataSchema = new DataSchema();
        //        dataSchema.Name = serverConnection.DatabaseName;
    
        //        Smo.Server server = new Smo.Server();

        //        var database = server.Databases[serverConnection.DatabaseName];
        //        foreach (Smo.Table table in database.Tables)
        //        {
        //            Table dataTable = new Table(dataSchema);
        //            foreach (Smo.Column column in table.Columns)
        //            {
        //                dataTable.Columns.Add(CreateColumn(dataSchema, column));
        //            }
        //        }
        //    }
        //}

        //!!
        //private Column CreateColumn(DataSchema dataSchema, Smo.Column column)
        //{
        //    ColumnType dataColumnType = GetColumnType(dataSchema, column);
        //    Column dataColumn = new Column();
        //    dataColumn.Name = column.Name;
        //    dataColumn.ColumnType = dataColumnType;
        //    dataColumn.CanBeNull = column.Nullable;
        //    dataColumn.InPrimaryKey = column.InPrimaryKey;
        //}

        //private ColumnType GetColumnType(DataSchema dataSchema, Smo.Column column)
        //{
        //    !!!!
        //}
    }
}

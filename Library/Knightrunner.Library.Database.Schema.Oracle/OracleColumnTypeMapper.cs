using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Oracle
{
    internal class OracleColumnTypeMapper : IColumnTypeMapper
    {
        public string GetColumnTypeString(TargetSystem targetSystem, Column column)
        {
            string result = MapColumnType(targetSystem, column);
            if (!column.CanBeNull)
            {
                result += " not null";
            }

            return result;
        }

        private string MapColumnType(TargetSystem targetSystem, Column column)
        {
            ColumnType columnType = column.ColumnType;
            Target target = null;

            while (target == null && columnType != null)
            {
                target = columnType.Targets[targetSystem.Name];
                if (target == null)
                {
                    columnType = columnType.BaseType;
                }
            }

            if (target == null)
            {
                throw new DataSchemaException("Target type not found for column " + column.Name);
            }

            string typeString = target.DataType;
            return MacroResolver.Resolve(column, typeString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Linq
{
    public class LinqColumnTypeMapper : IColumnTypeMapper
    {
        public string GetColumnTypeString(TargetSystem targetSystem, Column column)
        {
            return MapColumnType(targetSystem, column);
        }

        private string MapColumnType(TargetSystem targetSystem, Column column)
        {
            ColumnType columnType = column.ColumnType;
            Target target = null;
            string enumTypeName = null;

            while (target == null && columnType != null && enumTypeName == null)
            {
                enumTypeName = columnType.EnumTypeName;
                target = columnType.Targets[targetSystem.Name];
                if (target == null)
                {
                    columnType = columnType.BaseType;
                }
            }

            string typeString;
            if (enumTypeName != null)
            {
                typeString = enumTypeName;
            }
            else
            {
                if (target == null)
                {
                    throw new DataSchemaException("Target type not found for column " + column.Name);
                }
                typeString = target.DataType;
            }
            return MacroResolver.Resolve(column, typeString);
        }
    }
}

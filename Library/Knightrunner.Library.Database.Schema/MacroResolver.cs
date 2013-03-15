using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public static class MacroResolver
    {
        public static string Resolve(Column column, string typeString)
        {
            string result = typeString;
            if (column.ColumnType.MaxLength.HasValue)
            {
                result = result.Replace("%maxLength%", column.ColumnType.MaxLength.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (column.ColumnType.Precision.HasValue)
            {
                result = result.Replace("%precision%", column.ColumnType.Precision.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (column.ColumnType.Scale.HasValue)
            {
                result = result.Replace("%scale", column.ColumnType.Scale.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            return result;
        }


    }
}

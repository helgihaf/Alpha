using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class ColumnCollection : CollectionBase
    {
        public void Add(Column column)
        {
            List.Add(column);
        }

        public Column this[int index]
        {
            get
            {
                return (Column)List[index];
            }
        }

        public Column this[string columnName]
        {
            get
            {
                foreach (Column column in List)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Compare(column.Name, columnName) == 0)
                    {
                        return column;
                    }
                }

                return null;
            }
        }
    }
}

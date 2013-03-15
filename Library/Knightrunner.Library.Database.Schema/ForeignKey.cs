using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public class ForeignKey
    {
        public ForeignKey()
        {
            Columns = new List<ColumnPair>();
        }

        public string Name { get; set; }

        public class ColumnPair
        {
            public Column FromColumn { get; set; }
            public Column ToColumn { get; set; }
        }

        public Table FromTable { get; set; }

        public Table ToTable { get; set; }

        public List<ColumnPair> Columns { get; private set; }

        public AssociationProperty AssociationProperty { get; set; }

        internal string FromColumnsToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append(Columns[i].FromColumn.Name);
            }
            return sb.ToString();
        }

        internal string ToColumnsToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append(Columns[i].ToColumn.Name);
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Database.Schema
{
    public class NameFormats
    {
        public NameFormats()
        {
            ForeignKeyFormatString = "{0}_{2}_fk";
            PrimaryKeyFormatString = "{0}_pk";
            UniqueIndexFormatString = "{0}_{1}_uq";
            IndexFormatString = "{0}_{1}_ix";
        }

        public string ForeignKeyFormatString { get; set; }
        public string PrimaryKeyFormatString { get; set; }
        public string UniqueIndexFormatString { get; set; }
        public string IndexFormatString { get; set; }
    }
}

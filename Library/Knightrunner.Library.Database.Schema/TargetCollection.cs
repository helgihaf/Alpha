using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class TargetCollection : KeyedItemCollection<string, Target>
    {
        private ColumnType owner;

        public TargetCollection(ColumnType owner)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            this.owner = owner;
        }
    }
}

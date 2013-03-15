using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core.Collections;
using System.Diagnostics;

namespace Knightrunner.Library.Database.Schema
{
    public class ColumnTypeCollection : DynamicKeyedItemCollection<string, ColumnType>
    {
        private DataSchema owner;

        public ColumnTypeCollection(DataSchema owner)
        {
            this.owner = owner;
        }

        protected override void VerifyItem(ColumnType item)
        {
            base.VerifyItem(item);
            Debug.Assert(item.DataSchema == null);
        }

        protected override void AttachItem(ColumnType item)
        {
            item.DataSchema = owner;
            base.AttachItem(item);
        }

        protected override void DetachItem(ColumnType item)
        {
            item.DataSchema = null;
            base.DetachItem(item);
        }
    }

}

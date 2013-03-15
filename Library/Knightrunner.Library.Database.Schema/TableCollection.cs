using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core.Collections;
using System.Diagnostics;

namespace Knightrunner.Library.Database.Schema
{
    public class TableCollection : DynamicKeyedItemCollection<string, Table>
    {
        private DataSchema owner;

        public TableCollection(DataSchema owner)
        {
            this.owner = owner;
        }

        protected override void VerifyItem(Table item)
        {
            base.VerifyItem(item);
            Debug.Assert(item.DataSchema == null);
        }

        protected override void AttachItem(Table item)
        {
            item.DataSchema = owner;
            base.AttachItem(item);
        }

        protected override void DetachItem(Table item)
        {
            item.DataSchema = null;
            base.DetachItem(item);
        }
    }
}

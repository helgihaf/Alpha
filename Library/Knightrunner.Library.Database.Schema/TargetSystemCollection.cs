using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class TargetSystemCollection :  DynamicKeyedItemCollection<string, TargetSystem>
    {
        private DataSchema owner;

        public TargetSystemCollection(DataSchema owner)
        {
            this.owner = owner;
        }
    }
}

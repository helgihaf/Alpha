using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class TargetSystem : DynamicKeyedItem<string>
    {
        public DataSchema DataSchema
        {
            get;
            internal set;
        }

        public string Name
        {
            get { return Key; }
            set { Key = value; }
        }

        public void Verify(IVerificationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.TargetSystemNameEmpty));
            }
        }
    }
}

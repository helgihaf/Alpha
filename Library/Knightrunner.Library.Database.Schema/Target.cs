using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;
using System.Collections.Specialized;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class Target : IKeyedItem<string>
    {
        public Target()
        {
            ExtendedProperties = new NameValueCollection();
        }

        public string Key
        {
            get { return TargetSystem.Name; }
        }

        public TargetSystem TargetSystem { get; set; }
        public string DataType { get; set; }
        public string DataTypeWhenReferenced { get; set; }
        public string DotNetType { get; set; }
        public bool DotNetTypeNullable { get; set; }

        public NameValueCollection ExtendedProperties { get; private set; }

        public void Verify(IVerificationContext context)
        {
            if (TargetSystem == null)
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.TargetNoTargetSystem));
            }
            if (string.IsNullOrWhiteSpace(DataType))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.TargetDataTypeEmpty));
            }
            if (string.IsNullOrWhiteSpace(DotNetType))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.DotNetTypeEmpty));
            }
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Verification
{
    public class VerificationTableMessage : VerificationMessage
    {
        public string Table { get; private set; }
        public string Column { get; private set; }


        public VerificationTableMessage(Severity severity, string table, string text)
            : base(severity, text)
        {
            this.Table = table;
        }

        public VerificationTableMessage(Severity severity, string table, string column, string text)
            : base(severity, text)
        {
            this.Table = table;
            this.Column = column;
        }


        protected override string MessageContext
        {
            get
            {
                string msg = Table;
                if (Column != null)
                    msg += "." + Column;

                return "[" + msg + "]";
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Verification
{
    public class VerificationMessage
    {
        public Severity Severity { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; private set; }

        public VerificationMessage()
            : this(Severity.Error, null)
        {
        }

        public VerificationMessage(Severity severity, string text)
        {
            this.Severity = severity;
            this.Text = text;
            DateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public virtual string ToString(bool includeTimeStamp)
        {
            string context = MessageContext;

            return
                string.Format
                (
                    System.Globalization.CultureInfo.InvariantCulture,
                    "{0}{1,-8}: {2}{3}",
                    includeTimeStamp ? DateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + " " : string.Empty,
                    Severity.ToString(),
                    context != null ? context + " " : string.Empty,
                    Text
                );
        }

        
        protected virtual string MessageContext
        {
            get
            {
                return null;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Knightrunner.Library.Database.Schema.Verification
{
    public interface IVerificationContext
    {
        void Clear();
        void Add(VerificationMessage message);
        ReadOnlyCollection<VerificationMessage> Entries { get; }
        bool HasErrors { get; }
    }
}

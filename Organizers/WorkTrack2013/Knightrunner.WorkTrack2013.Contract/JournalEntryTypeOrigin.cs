using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Knightrunner.WorkTrack2013.Contract
{
    [DataContract(Namespace = Constants.DataNamespace)]
    public enum JournalEntryTypeOrigin
    {
        [EnumMember]
        Undefined,
        [EnumMember]
        Start,
        [EnumMember]
        End,
    }
}

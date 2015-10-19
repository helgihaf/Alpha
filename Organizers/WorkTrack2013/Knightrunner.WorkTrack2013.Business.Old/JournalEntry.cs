using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Knightrunner.WorkTrack2013.Business
{
    [DataContract(Namespace = Constants.DataNamespace)]
    public class JournalEntry
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public JournalEntryType Type { get; set; }
        [DataMember]
        public JournalEntryTypeOrigin TypeOrigin { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
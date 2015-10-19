using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Knightrunner.WorkTrack2013.Business
{
    [DataContract(Namespace=Constants.DataNamespace)]
    public class Activity
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public TimeSpan? Duration { get; set; }
        [DataMember]
        public string ProjectPublicId { get; set; }
        [DataMember]
        public string ActivityPublicId { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
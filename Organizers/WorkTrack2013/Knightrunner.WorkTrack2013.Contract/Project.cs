using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Knightrunner.WorkTrack2013.Contract
{
    [DataContract(Namespace = Constants.DataNamespace)]
    public class Project
    {
        [DataMember]
        public string PublicId { get; set; }

        [DataMember]
        public string Text { get; set; }
        
        [DataMember]
        public bool Closed { get; set; }

        [DataMember]
        public string ParentProjectId { get; set; }
    }
}

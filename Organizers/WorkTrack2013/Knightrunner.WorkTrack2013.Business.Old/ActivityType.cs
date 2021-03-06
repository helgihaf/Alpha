﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Knightrunner.WorkTrack2013.Business
{
    [DataContract(Namespace = Constants.DataNamespace)]
    public class ActivityType
    {
        [DataMember]
        public string PublicId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool Closed { get; set; }
    }
}
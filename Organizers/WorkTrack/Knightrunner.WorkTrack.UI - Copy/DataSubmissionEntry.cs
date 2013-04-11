using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.WorkTrack.Model;

namespace Knightrunner.WorkTrack.UI
{
    public class DataSubmissionEntry<T>
    {
        public SubmissionType SubmissionType { get; set; }
        public T NewItem { get; set; }
        public T OldItem { get; set; }
    }
}

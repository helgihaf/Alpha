using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.WorkTrack.UI
{
    /// <summary>
    /// A generic selector for data to be displayed in a user interface.
    /// </summary>
    public class DataSelector
    {
        public DataSelector()
        {
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public object Object { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

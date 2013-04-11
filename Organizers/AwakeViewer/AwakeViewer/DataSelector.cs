using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwakeViewer
{
    internal class DataSelector
    {
        public DataSelector()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public object Object { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

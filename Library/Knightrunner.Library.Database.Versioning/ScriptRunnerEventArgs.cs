using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public class ScriptRunnerEventArgs : EventArgs
    {
        public Script Script { get; set; }
        public string CommandText { get; set; }
    }
}

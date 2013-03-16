using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.InteractiveFiction
{
    /// <summary>
    /// An object with a unique name.
    /// </summary>
    public interface INamed
    {
        string Name { get; set; }
    }
}

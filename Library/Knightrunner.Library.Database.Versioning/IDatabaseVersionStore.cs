using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public interface IDatabaseVersionStore
    {
        Version Version { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marson.Compare.Core
{
    public enum CompareStatus
    {
        Unknown,
        LeftNewer,
        RightNewer,
        NotEqual,
        Equal,
        LeftOrphant,
        RightOrphant,
    }
}

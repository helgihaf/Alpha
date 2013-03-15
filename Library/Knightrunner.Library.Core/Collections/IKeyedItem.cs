using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core.Collections
{
    public interface IKeyedItem<T>
    {
        T Key { get; }
    }
}

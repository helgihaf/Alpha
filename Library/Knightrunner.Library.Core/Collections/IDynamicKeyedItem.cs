using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core.Collections
{
    public interface IDynamicKeyedItem<T> : IKeyedItem<T>
    {
        new T Key { get; set; }

        event EventHandler<PropertyChangingEventArgs<T>> KeyChanging;
        event EventHandler<PropertyChangedEventArgs<T>> KeyChanged;

    }
}

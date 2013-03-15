using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core.Collections
{
    public class DynamicKeyedItem<T> : IDynamicKeyedItem<T>
    {

        private T key;

        public T Key
        {
            get { return key; }

            set
            {
                if (object.Equals(key, value))
                {
                    return;
                }

                if (!OnKeyChanging(key, value))
                {
                    throw new ArgumentException("Invalid property value");
                }
                var oldValue = key;
                key = value;
                OnKeyChanged(oldValue, key);
            }
        }

        public event EventHandler<PropertyChangingEventArgs<T>> KeyChanging;

        public event EventHandler<PropertyChangedEventArgs<T>> KeyChanged;


        protected virtual bool OnKeyChanging(T currentValue, T newValue)
        {
            bool result = true;
            if (KeyChanging != null)
            {
                var e = new PropertyChangingEventArgs<T> { OldValue = currentValue, NewValue = newValue };
                KeyChanging(this, e);
                result = !e.Cancel;
            }

            return result;
        }

        protected virtual void OnKeyChanged(T oldValue, T currentValue)
        {
            if (KeyChanged != null)
            {
                var e = new PropertyChangedEventArgs<T> { OldValue = oldValue, NewValue = currentValue };
                KeyChanged(this, e);
            }
        }


    }
}

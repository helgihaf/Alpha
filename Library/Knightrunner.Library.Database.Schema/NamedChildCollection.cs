using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Knightrunner.Library.Database.Schema
{
    public class NamedChildCollection<T> : ICollection<T>, IEnumerable<T>
        where T : NamedChild
    {
        private IParent owner;
        private Dictionary<string, T> dictionary = new Dictionary<string, T>();
        private EventHandler<PropertyChangingEventArgs<string>> nameChangingEventHandler;
        private EventHandler<PropertyChangedEventArgs<string>> nameChangedEventHandler;

        public NamedChildCollection(IParent owner)
        {
            this.owner = owner;
            nameChangingEventHandler += new EventHandler<PropertyChangingEventArgs<string>>(item_NameChanging);
            nameChangedEventHandler += new EventHandler<PropertyChangedEventArgs<string>>(item_NameChanged);
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (dictionary.ContainsKey(item.Name))
            {
                throw new ArgumentException("An item with this name already exists in the collection");
            }

            Debug.Assert(item.Parent == null);

            dictionary.Add(item.Name, item);
            AttachItem(item);

        }

        private void item_NameChanging(object sender, PropertyChangingEventArgs<string> e)
        {
            e.Cancel = dictionary.ContainsKey(e.NewValue);
        }

        private void item_NameChanged(object sender, PropertyChangedEventArgs<string> e)
        {
            var item = dictionary[e.OldValue];
            dictionary.Remove(e.OldValue);
            dictionary.Add(e.NewValue, item);
        }


        public void Clear()
        {
            foreach (var item in dictionary.Values)
            {
                DetachItem(item);
            }

            dictionary.Clear();
        }

        public bool Contains(string name)
        {
            return dictionary.ContainsKey(name);
        }

        public bool Contains(T item)
        {
            return dictionary.Values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dictionary.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string name)
        {
            bool result = false;
            T item;
            if (dictionary.TryGetValue(name, out item))
            {
                DetachItem(item);
                dictionary.Remove(name);
                result = true;
            }

            return result;
        }

        public bool Remove(T item)
        {
            bool result = false;
            T existingItem;
            if (dictionary.TryGetValue(item.Name, out existingItem))
            {
                Debug.Assert(object.Equals(existingItem, item));
                DetachItem(existingItem);
                dictionary.Remove(existingItem.Name);
                result = true;
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)dictionary.Values).GetEnumerator();
        }

        public T this[string name]
        {
            get
            {
                T result;
                if (!dictionary.TryGetValue(name, out result))
                {
                    result = null;
                }

                return result;
            }
        }

        private void AttachItem(T item)
        {
            item.Parent = owner;
            item.NameChanging += nameChangingEventHandler;
            item.NameChanged += nameChangedEventHandler;
        }

        private void DetachItem(T item)
        {
            item.Parent = null;
            item.NameChanging -= nameChangingEventHandler;
            item.NameChanged -= nameChangedEventHandler;
        }

    }
}

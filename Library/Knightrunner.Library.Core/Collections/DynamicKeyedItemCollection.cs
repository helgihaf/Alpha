using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Knightrunner.Library.Core.Collections
{
    public class DynamicKeyedItemCollection<TKey, TItem> : ICollection<TItem>, IEnumerable<TItem>
        where TItem : IDynamicKeyedItem<TKey>
    {
        private Dictionary<TKey, TItem> dictionary;
        private EventHandler<PropertyChangingEventArgs<TKey>> keyChangingEventHandler;
        private EventHandler<PropertyChangedEventArgs<TKey>> keyChangedEventHandler;

        public DynamicKeyedItemCollection()
        {
            dictionary = new Dictionary<TKey, TItem>(); 
            keyChangingEventHandler += new EventHandler<PropertyChangingEventArgs<TKey>>(item_KeyChanging);
            keyChangedEventHandler += new EventHandler<PropertyChangedEventArgs<TKey>>(item_KeyChanged);
        }

        public DynamicKeyedItemCollection(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TItem>(comparer);
            keyChangingEventHandler += new EventHandler<PropertyChangingEventArgs<TKey>>(item_KeyChanging);
            keyChangedEventHandler += new EventHandler<PropertyChangedEventArgs<TKey>>(item_KeyChanged);
        }

        public void Add(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (dictionary.ContainsKey(item.Key))
            {
                throw new ArgumentException("An item with this key already exists in the collection");
            }

            VerifyItem(item);

            dictionary.Add(item.Key, item);
            AttachItem(item);

        }

        protected virtual void VerifyItem(TItem item)
        {
        }

        private void item_KeyChanging(object sender, PropertyChangingEventArgs<TKey> e)
        {
            e.Cancel = dictionary.ContainsKey(e.NewValue);
        }

        private void item_KeyChanged(object sender, PropertyChangedEventArgs<TKey> e)
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

        public bool Contains(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool Contains(TItem item)
        {
            return dictionary.Values.Contains(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
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

        public bool Remove(TKey key)
        {
            bool result = false;
            TItem item;
            if (dictionary.TryGetValue(key, out item))
            {
                DetachItem(item);
                dictionary.Remove(key);
                result = true;
            }

            return result;
        }

        public bool Remove(TItem item)
        {
            bool result = false;
            TItem existingItem;
            if (dictionary.TryGetValue(item.Key, out existingItem))
            {
                Debug.Assert(object.Equals(existingItem, item));
                DetachItem(existingItem);
                dictionary.Remove(existingItem.Key);
                result = true;
            }

            return result;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)dictionary.Values).GetEnumerator();
        }

        public TItem this[TKey key]
        {
            get
            {
                TItem result;
                if (!dictionary.TryGetValue(key, out result))
                {
                    result = default(TItem);
                }

                return result;
            }
        }

        protected virtual void AttachItem(TItem item)
        {
            //item.Parent = owner;
            item.KeyChanging += keyChangingEventHandler;
            item.KeyChanged += keyChangedEventHandler;
        }

         protected virtual void DetachItem(TItem item)
        {
            //item.Parent = null;
            item.KeyChanging -= keyChangingEventHandler;
            item.KeyChanged -= keyChangedEventHandler;
        }
    }
}

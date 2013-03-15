using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core.Collections
{
    public class KeyedItemCollection<TKey, TItem>  : ICollection<TItem>, IEnumerable<TItem>
        where TItem : IKeyedItem<TKey>
    {
        private Dictionary<TKey, TItem> dictionary;

        public KeyedItemCollection()
        {
            dictionary = new Dictionary<TKey, TItem>();
        }

        public KeyedItemCollection(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TItem>(comparer);
        }

        public void Add(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            dictionary.Add(item.Key, item);
        }


        public void Clear()
        {
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
            return dictionary.Remove(key);
        }

        public bool Remove(TItem item)
        {
            bool result = false;
            TItem existingItem;
            if (dictionary.TryGetValue(item.Key, out existingItem))
            {
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

    }
}

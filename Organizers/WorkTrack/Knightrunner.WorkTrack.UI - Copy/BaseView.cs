using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Model;

namespace Knightrunner.WorkTrack.UI
{
    public partial class BaseView : UserControl
    {
        private Dictionary<Guid, DataEntity> loadedItems = new Dictionary<Guid, DataEntity>();

        public BaseView()
        {
            InitializeComponent();
        }


        protected void CopyLoadedItems(IEnumerable<DataEntity> items)
        {
            loadedItems.Clear();
            foreach (var item in items)
            {
                var clone = item.Clone();
                loadedItems.Add(clone.Id, clone);
            }
        }





        protected IEnumerable<DataSubmissionEntry<T>> GetSubmissions<T>(IEnumerable<T> items) where T : DataEntity
        {
            List<DataSubmissionEntry<T>> submissions = new List<DataSubmissionEntry<T>>();

            var itemsToDelete = new Dictionary<Guid, T>();
            foreach (var item in items)
            {
                itemsToDelete.Add(item.Id, item);
            }

            foreach (var item in items)
            {
                DataEntity loadedItem;
                if (loadedItems.TryGetValue(item.Id, out loadedItem))
                {
                    // Determine if changed
                    if (!item.Equals(loadedItem))
                    {
                        submissions.Add(new DataSubmissionEntry<T> { SubmissionType = SubmissionType.Update, NewItem = item, OldItem = (T)loadedItem });
                    }
                    itemsToDelete.Remove(item.Id);
                }
                else
                {
                    submissions.Add(new DataSubmissionEntry<T> { SubmissionType = SubmissionType.Insert, NewItem = item });
                    itemsToDelete.Remove(item.Id);
                }
            }

            foreach (var item in itemsToDelete.Values)
            {
                submissions.Add(new DataSubmissionEntry<T> { SubmissionType = SubmissionType.Delete, OldItem = item });
            }

            return submissions;
        }


        protected bool IsDirty<T>(IEnumerable<T> items) where T : DataEntity
        {
            if (loadedItems.Count == 0)
            {
                return false;
            }

            var itemsToDelete = new Dictionary<Guid, T>();
            foreach (var item in items)
            {
                itemsToDelete.Add(item.Id, item);
            }

            foreach (var item in items)
            {
                DataEntity loadedItem;
                if (loadedItems.TryGetValue(item.Id, out loadedItem))
                {
                    // Determine if changed
                    if (!item.Equals(loadedItem))
                    {
                        return true;
                    }
                    itemsToDelete.Remove(item.Id);
                }
                else
                {
                    return true;
                }
            }

            return itemsToDelete.Count > 0;
        }


    }
}

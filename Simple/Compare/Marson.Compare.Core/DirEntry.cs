using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    public class DirEntry : Entry
    {
        private DirItem LeftDirItem { get { return (DirItem)Left;  } }
        private DirItem RightDirItem { get { return (DirItem)Right; } }

        internal void Populate()
        {
            PopulateChildEntries();
        }

        private void PopulateChildEntries()
        {
            List<Item> leftList = LeftDirItem != null ? GetDirItems(LeftDirItem.FullPath) : new List<Item>();
            List<Item> rightList = RightDirItem != null ? GetDirItems(RightDirItem.FullPath) : new List<Item>();
            PopulateChildEntries(leftList, rightList, () => new DirEntry());

            leftList = LeftDirItem != null ? GetFileItems(LeftDirItem.FullPath) : new List<Item>();
            rightList = RightDirItem != null ? GetFileItems(RightDirItem.FullPath) : new List<Item>();
            PopulateChildEntries(leftList, rightList, () => new FileEntry());
        }

        internal override void Compare(IFileComparer fileComparer)
        {
            if (ChildEntries.Count > 0)
            {
                foreach (var entry in ChildEntries)
                {
                    entry.Compare(fileComparer);
                    foreach (var status in entry.CompareStatuses)
                    {
                        CompareStatuses.Add(status);
                    }
                }
            }
            else
            {
                if (Left != null && Right != null)
                {
                    CompareStatuses.Add(CompareStatus.Equal);
                }
                else if (Left != null)
                {
                    CompareStatuses.Add(CompareStatus.LeftOrphant);
                }
                else
                {
                    CompareStatuses.Add(CompareStatus.RightOrphant);
                }
            }
        }

        private void PopulateChildEntries(List<Item> leftItems, List<Item> rightItems, Func<Entry> createEntryFunc)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            while (leftIndex < leftItems.Count || rightIndex < rightItems.Count)
            {
                Item leftItem = leftIndex < leftItems.Count ? leftItems[leftIndex] : null;
                Item rightItem = rightIndex < rightItems.Count ? rightItems[rightIndex] : null;
                var newEntry = createEntryFunc();

                if (leftItem != null && rightItem != null)
                {
                    int nameCompareResult = string.Compare(leftItem.Name, rightItem.Name, true);
                    if (nameCompareResult == 0)
                    {
                        newEntry.Left = leftItem;
                        newEntry.Right = rightItem;
                        leftIndex++;
                        rightIndex++;
                    }
                    else if (nameCompareResult < 0)
                    {
                        newEntry.Left = leftItem;
                        leftIndex++;
                    }
                    else if (nameCompareResult > 0)
                    {
                        newEntry.Right = rightItem;
                        rightIndex++;
                    }
                }
                else if (leftItem == null)
                {
                    newEntry.Right = rightItem;
                    rightIndex++;
                }
                else if (rightItem == null)
                {
                    newEntry.Left = leftItem;
                    leftIndex++;
                }
                else
                {
                    throw new Exception("Fatal internal error: both items are null");
                }

                var newDirEntry = newEntry as DirEntry;
                if (newDirEntry != null)
                {
                    newDirEntry.PopulateChildEntries();
                }
                ChildEntries.Add(newEntry);
            }
        }


        private List<Item> GetDirItems(string baseDirPath)
        {
            var dirItems = new List<Item>();
            foreach (var dirPath in Directory.GetDirectories(baseDirPath))
            {
                dirItems.Add(new DirItem(dirPath));
            }
            return dirItems;
        }

        private List<Item> GetFileItems(string baseDirPath)
        {
            var fileItems = new List<Item>();
            foreach (var filePath in Directory.GetFiles(baseDirPath))
            {
                fileItems.Add(new FileItem(filePath));
            }

            return fileItems.OrderBy(f => f.Name).ToList();
        }

    }
}

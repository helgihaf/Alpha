using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace WorkTrackLite
{
    internal class DataStore
    {
        private string dataFilePath;
        private List<WorkEntry> workEntries;

        public string DataFilePath
        {
            get
            {
                if (dataFilePath == null)
                {
                    dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkTrackLite.xml");
                }
                return dataFilePath;
            }
        }

        public void SaveWorkEntry(WorkEntry entry)
        {
            var entryList = GetWorkEntryList();
            entryList.Add(entry);
            SaveWorkEntries(entryList);
        }



        public WorkEntry[] GetWorkEntries()
        {
            return GetWorkEntryList().ToArray();
        }


        private List<WorkEntry> GetWorkEntryList()
        {
            if (workEntries == null)
            {
                workEntries = new List<WorkEntry>();
                if (File.Exists(DataFilePath))
                {
                    var xdoc = XDocument.Load(DataFilePath);
                    foreach (var xWorkEntryElement in xdoc.Element("WorkEntries").Elements("WorkEntry"))
                    {
                        workEntries.Add(CreateWorkItemFrom(xWorkEntryElement));
                    }
                }
            }

            return workEntries;
        }



        private void SaveWorkEntries(List<WorkEntry> entryList)
        {
            entryList.Sort((a, b) => DateTime.Compare(a.DateTime, b.DateTime));
            var xWorkEntries = new XElement("WorkEntries");
            foreach (var workEntry in entryList)
            {
                xWorkEntries.Add(CreateXElementFor(workEntry));
            }
            XDocument xdoc = new XDocument();
            xdoc.Add(xWorkEntries);
            xdoc.Save(DataFilePath);
        }


        private XElement CreateXElementFor(WorkEntry workEntry)
        {
            return new XElement
            (
                "WorkEntry",
                new XElement("DateTime", workEntry.DateTime),
                new XElement("Description", workEntry.Description)
            );
        }


        private WorkEntry CreateWorkItemFrom(XElement xWorkEntryElement)
        {
            var workEntry = new WorkEntry();
            workEntry.DateTime = DateTime.Parse(xWorkEntryElement.Element("DateTime").Value);
            workEntry.Description = xWorkEntryElement.Element("Description").Value;

            return workEntry;
        }


    }
}

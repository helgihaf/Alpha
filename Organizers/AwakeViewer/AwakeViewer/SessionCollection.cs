using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace AwakeViewer
{
    public class SessionCollection : ICollection<Session>
    {
        private SortedList<DateTime, Session> sessions = new SortedList<DateTime, Session>();

        #region ICollection<Session> Members

        public void Add(Session item)
        {
            sessions.Add(item.Start, item);
        }

        public void Clear()
        {
            sessions.Clear();
        }

        public bool Contains(Session item)
        {
            return sessions.Contains(new KeyValuePair<DateTime,Session>(item.Start, item));
        }

        public void CopyTo(Session[] array, int arrayIndex)
        {
            sessions.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return sessions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Session item)
        {
            return sessions.Remove(item.Start);
        }

        #endregion

        public void Update(DateTime oldStartTime, Session item)
        {
            sessions.Remove(oldStartTime);
            Add(item);
        }

        #region IEnumerable<Session> Members

        public IEnumerator<Session> GetEnumerator()
        {
            return sessions.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sessions.Values.GetEnumerator();
        }

        #endregion


        //public Session this[DateTime startDateTime]
        //{
        //    get
        //    {
        //        int index = IndexOf(startDateTime);
        //        if (index != -1)
        //        {
        //            return sessions[index];
        //        }
        //        else
        //        {
        //            throw new IndexOutOfRangeException();
        //        }
        //    }
        //}

        //private int IndexOf(DateTime startDateTime)
        //{
        //    return sessions
        //}


        public void Save()
        {
            string filePath = GetFilePath();
            PrepareFilePath(filePath);

            List<Session> sessionList = new List<Session>(sessions.Values);
            XmlSerializer serializer = new XmlSerializer(sessionList.GetType());
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, sessionList);
            }
        }


        public static SessionCollection Load()
        {
            string filePath = GetFilePath();
            SessionCollection sessionCollection = new SessionCollection();
            if (File.Exists(filePath))
            {
                List<Session> sessionList;
                XmlSerializer serializer = new XmlSerializer(typeof(List<Session>));
                using (StreamReader writer = new StreamReader(filePath))
                {
                    sessionList = (List<Session>)serializer.Deserialize(writer);
                }

                foreach (var session in sessionList)
                {
                    sessionCollection.Add(session);
                }
            }
            return sessionCollection;
        }

        private static string GetFilePath()
        {
            const string fileTail = @"Knightrunner\AwakeViewer\sessions.xml";
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), fileTail);
        }

        private void PrepareFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            string dir = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(dir))
                return;

            if (Directory.Exists(dir))
                return;

            Directory.CreateDirectory(dir);
        }


        //public void Sort()
        //{
        //    sessions.Sort
        //        (
        //            (a, b) => DateTime.Compare(a.Start, b.Start)
        //        );
        //}



        public void UpdateFromEventLog()
        {
            // Read the event log and match or add to existing list
            EventLog log = new EventLog("System");

            DateTime? maxDate = null;
            if (sessions.Count > 0)
            {
                maxDate = sessions.Values[sessions.Count - 1].Start;
            }

            List<EventLogEntry> filteredEventLogEntries = new List<EventLogEntry>(FilterLogEntries(log.Entries, maxDate));

            // First collect all new sessions
            foreach (EventLogEntry entry in filteredEventLogEntries)
            {
                if (entry.InstanceId == 12)
                {
                    // Windows is starting
                    DateTime startTime = entry.TimeGenerated;
                    Session session;
                    if (!sessions.TryGetValue(startTime, out session))
                    {
                        // New session
                        session = new Session { Start = entry.TimeGenerated };
                        Add(session);
                    }
                }
            }

            // Now find end times
            foreach (EventLogEntry entry in filteredEventLogEntries)
            {
                if (entry.InstanceId == 13)
                {
                    // Session is ending
                    DateTime endTime = entry.TimeGenerated;
                    int lastIndex = -1;
                    for (int i = 0; i < sessions.Count; i++)
                    {
                        if (sessions.Values[i].Start > endTime)
                        {
                            break;
                        }
                        lastIndex = i;
                    }

                    // Assign end time if not already set
                    if (lastIndex != -1 && lastIndex < sessions.Count && !sessions.Values[lastIndex].End.HasValue)
                    {
                        sessions.Values[lastIndex].End = endTime;
                    }
                }
            }

        }

        private IEnumerable<EventLogEntry> FilterLogEntries(EventLogEntryCollection logEntries, DateTime? maxDate)
        {
            foreach (EventLogEntry entry in logEntries)
            {
                if (entry.Source == "Microsoft-Windows-Kernel-General" && (entry.InstanceId == 12 || entry.InstanceId == 13)
                    && (maxDate == null || entry.TimeGenerated > maxDate.Value))
                {
                    yield return entry;
                }
            }
        }

        //private IDictionary<DateTime, Session> BuildSessionDictionary()
        //{
        //    SortedDictionary<DateTime, Session> dictionary = new Dictionary<DateTime, Session>();
        //    for (int i = 0; i < sessions.Count; )
        //    {
        //        try
        //        {
        //            dictionary.Add(sessions[i].Start, sessions[i]);
        //            i++;
        //        }
        //        catch (ArgumentException)
        //        {
        //            // Key already exists - remove it from the original
        //            sessions.RemoveAt(i);
        //        }
        //    }

        //    return dictionary;
        //}


        //private SortedList<DateTime, Session> BuildSortedSessionList()
        //{
        //    SortedList<DateTime, Session> list = new SortedList<DateTime, Session>();
        //    for (int i = 0; i < sessions.Count; )
        //    {
        //        try
        //        {
        //            list.Add(sessions[i].Start, sessions[i]);
        //            i++;
        //        }
        //        catch (ArgumentException)
        //        {
        //            // Key already exists - remove it from the original
        //            sessions.RemoveAt(i);
        //        }
        //    }

        //    return list;
        //}

    }
}

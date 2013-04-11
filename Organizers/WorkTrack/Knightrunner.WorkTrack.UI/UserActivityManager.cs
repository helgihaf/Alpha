using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;


namespace Knightrunner.WorkTrack.UI
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    public class UserActivityManager
    {
        private const uint IdleThresholdMs = 4 * 60 * 1000;

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        
        private List<UserActivityEntry> entries = new List<UserActivityEntry>();
        private bool isIdle = false;

        public void Initialize(DateTime startOfEvents)
        {
            LoadFromEventLog(startOfEvents);

            CheckIdle();
        }

        private void LoadFromEventLog(DateTime startOfEvents)
        {


        string eventID = "5312";
        string LogSource = "Microsoft-Windows-Kernel-General";  
        string sQuery = "*[System/EventID=" + eventID + "]";

        var elQuery = new EventLogQuery(LogSource, PathType.LogName, sQuery);
        var elReader = new System.Diagnostics.Eventing.Reader.EventLogReader(elQuery);

        List<EventRecord> eventList = new List<EventRecord>();
        for (EventRecord eventInstance = elReader.ReadEvent();
            null != eventInstance; eventInstance = elReader.ReadEvent())
        {
            //Access event properties here:
            //eventInstance.LogName;
            //eventInstance.ProviderName;
            eventList.Add(eventInstance);
        }




            // Read the event log and match or add to existing list
            EventLog log = new EventLog("System");

            foreach (EventLogEntry entry in log.Entries)
            {
                if (entry.Source == "Microsoft-Windows-Kernel-General" && entry.TimeGenerated > startOfEvents)
                {
                    
   
                    (entry.InstanceId == 12 || entry.InstanceId == 13)
                    && (maxDate == null || entry.TimeGenerated > maxDate.Value))
                {
                    yield return entry;
                }
            }

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


        private void CheckIdle()
        {
            LASTINPUTINFO lastInput = new LASTINPUTINFO();
            lastInput.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInput);
            if (!GetLastInputInfo(ref lastInput))
            {
                return;
            }

            uint idleTicks = (uint)Environment.TickCount - lastInput.dwTime;
            if (idleTicks > IdleThresholdMs)
            {
                if (!isIdle)
                {
                    isIdle = true;
                    AddEntry(DateTime.Now.AddMilliseconds(-idleTicks), ActivityType.IdleStart);
                }
            }
            else
            {
                if (isIdle)
                {
                    isIdle = false;
                    AddEntry(DateTime.Now.AddMilliseconds(-idleTicks), ActivityType.IdleEnd);
                }
            }
        }

        private void AddEntry(DateTime dateTime, ActivityType activityType)
        {
            var entry = new UserActivityEntry { DateTime = dateTime, ActivityType = activityType };

            for (int i = entries.Count - 1; i >= 0; i--)
            {
                if (entries[i].DateTime < dateTime)
                {
                    entries.Insert(i + 1, entry);
                    return;
                }
            }

            entries.Add(entry);
        }
    }

    public class UserActivityEntry
    {
        public DateTime DateTime { get; set; }
        public ActivityType ActivityType { get; set; }
    }


    public enum ActivityType
    {
        Startup,
        Shutdown,
        Login,
        Logout,
        Hybernate,
        HybernateResume,
        Sleep,
        SleepResume,
        Dock,
        Undock,
        IdleStart,
        IdleEnd,
    }
}

using Knightrunner.WorkTrack2013.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeClient
{
    class TestData
    {
        private List<JournalEntry> journal;

        public List<JournalEntry> GetJournal()
        {
            if (journal == null)
            {
                journal = new List<JournalEntry>
                {
                    // User punches in
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T07:35:00"), Type = JournalEntryType.PunchClock, TypeOrigin = JournalEntryTypeOrigin.Start },
                    // Startup
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T07:43:00"), Type = JournalEntryType.OperatingSystem, TypeOrigin = JournalEntryTypeOrigin.Start },
                    // Login
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T07:45:00"), Type = JournalEntryType.UserSession, TypeOrigin = JournalEntryTypeOrigin.Start },
                    // Start outlook
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T07:48:00"), Type = JournalEntryType.Application, TypeOrigin = JournalEntryTypeOrigin.Start, Text = "Outlook"  },
                    // Users goes to meeting
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T08:45:00"), Type = JournalEntryType.Idle, TypeOrigin = JournalEntryTypeOrigin.Start  },
                    // Auto session lock
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T08:55:00"), Type = JournalEntryType.LockUnlock, TypeOrigin = JournalEntryTypeOrigin.Start  },
                    // Users back from meeting
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T09:55:00"), Type = JournalEntryType.Idle, TypeOrigin = JournalEntryTypeOrigin.End  },
                    // User unlocks session
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T09:55:04"), Type = JournalEntryType.LockUnlock, TypeOrigin = JournalEntryTypeOrigin.End  },
                    // Users goes to lunch
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T11:45:00"), Type = JournalEntryType.Idle, TypeOrigin = JournalEntryTypeOrigin.Start  },
                    // Auto session lock
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T11:55:00"), Type = JournalEntryType.LockUnlock, TypeOrigin = JournalEntryTypeOrigin.Start  },
                    // Users back from lunch
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T12:30:00"), Type = JournalEntryType.Idle, TypeOrigin = JournalEntryTypeOrigin.End  },
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T12:30:00"), Type = JournalEntryType.LockUnlock, TypeOrigin = JournalEntryTypeOrigin.End  },
                    // User punches out
                    new JournalEntry { DateTime = DateTime.Parse("2013-01-07T16:00:00"), Type = JournalEntryType.PunchClock, TypeOrigin = JournalEntryTypeOrigin.End },
                };
            }
            return journal;
        }
    }
}

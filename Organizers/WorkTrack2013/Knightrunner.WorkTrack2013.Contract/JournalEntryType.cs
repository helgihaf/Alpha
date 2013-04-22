using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Knightrunner.WorkTrack2013.Contract
{
    [DataContract(Namespace = Constants.DataNamespace)]
    public enum JournalEntryType
    {
        /// <summary>
        /// User entered some text. Use with JournalEntryTypeOrigin.Undefined.
        /// </summary>
        [EnumMember]
        User,
        
        /// <summary>
        /// Operating system started or was shut down. Use with JournalEntryTypeOrigin.Start and JournalEntryTypeOrigin.End.
        /// </summary>
        [EnumMember]
        OperatingSystem,
        
        /// <summary>
        /// Login/logout.
        /// </summary>
        [EnumMember]
        UserSession,

        /// <summary>
        /// User locks/unlocks session. Use with JournalEntryTypeOrigin.Start and JournalEntryTypeOrigin.End.
        /// </summary>
        [EnumMember]
        LockUnlock,

        /// <summary>
        /// System is determined to be idle (user not using his session for specific amount of time). Use with JournalEntryTypeOrigin.Start and JournalEntryTypeOrigin.End.
        /// </summary>
        [EnumMember]
        Idle,

        /// <summary>
        /// Application starts or stops. Use with JournalEntryTypeOrigin.Start and JournalEntryTypeOrigin.End.
        /// </summary>
        [EnumMember]
        Application,
        
        /// <summary>
        /// User punches in or out in a timekeeping system. Use with JournalEntryTypeOrigin.Start and JournalEntryTypeOrigin.End.
        /// </summary>
        [EnumMember]
        PunchClock,
    }
}
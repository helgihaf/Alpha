using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.UserAccounting
{
    public enum TransactionType
    {
        AccountCreated,    // DateTime
        UserInfoChanged,    // all changed data (full name, email, address...)
        Locked,                // Reason: manual, too many login attempts
        Unlocked,
        Approved,
        LogOn,
        LogOff,
        PasswordChanged,
        ExpirationDateChanged,
        InvalidPasswordOnLogOn,        // IP address, password
    }
}

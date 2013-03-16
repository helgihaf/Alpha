using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Knightrunner.Library.UserAccounting
{
    [DataContract]
    public enum AuthenticationResult
    {
        OK,
        InvalidUsernameOrPassword,
        AccountNotApproved,
        AccountLocked,
        AccountExpired
    }
}

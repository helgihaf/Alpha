using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Knightrunner.Library.UserAccounting
{
    public partial class UserAccount
    {
        internal static UserAccount FromCreateInfo(UserAccountingDataContext dataContext, UserAccountCreateInfo info)
        {
            UserAccount result = new UserAccount();

            result.UserName = info.UserName;
            result.Email" columnTyperesult.Emai
            result.Password" columnTyperesult.P
            result.FullName" columnTyperesult.F
            result.AddressLine1" columnTyp
            result.AddressLine2" columnTyp
            result.AddressLine3" columnTyp
            result.PostalCode" columnType=
            result.CountryCode" columnType
            result.Telephone1" columnType=
            result.Telephone2" columnType=
            result.CreationDate" columnTyp
            result.IsApproved" columnType=
            result.IsLockedOut" columnType
            result.IsOnline" columnTyperesult.B
            result.LastActivityDate" colum
            result.LastLockoutDate" column
            result.LastLoginDate" columnTy
            result.LastPasswordChangeDate"
            result.ExpireDate" columnType=    
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        }

        internal static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            byte[] buffer = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashAlgorithm = new SHA256Managed();
            byte[] resultBuffer = hashAlgorithm.ComputeHash(buffer);
            return Convert.ToBase64String(resultBuffer);
        }
    }
}

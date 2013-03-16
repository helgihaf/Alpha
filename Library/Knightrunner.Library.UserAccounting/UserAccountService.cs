using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.UserAccounting
{
    public class UserAccountService : IUserAccountService
    {
        public void CreateUserAccount(UserAccountCreateInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            using (UserAccountingDataContext dataContext = CreateDataContext())
            {
                UserAccount userAccount = UserAccount.FromCreateInfo(dataContext, info);
                dataContext.UserAccounts.InsertOnSubmit(userAccount);
                dataContext.SubmitChanges();
            }
        }

        
        public AuthenticationResult Autenticate(string userName, string password)
        {
            using (UserAccountingDataContext dataContext = CreateDataContext())
            {
                string hashedPassword = UserAccount.HashPassword(password);

                var authenticationAccount = dataContext.UserAccounts.FirstOrDefault
                (
                    userAccount => userAccount.UserName == userName && userAccount.Password == hashedPassword
                );

                if (authenticationAccount == null)
                {
                    return AuthenticationResult.InvalidUsernameOrPassword;
                }

                if (authenticationAccount.IsLockedOut)
                {
                    return AuthenticationResult.AccountLocked;
                }

                if (!authenticationAccount.IsApproved)
                {
                    return AuthenticationResult.AccountNotApproved;
                }

                DateTime now = DateTime.Now;

                if (authenticationAccount.ExpireDate.HasValue && authenticationAccount.ExpireDate.Value.Date <= now.Date)
                {
                    return AuthenticationResult.AccountExpired;
                }

                authenticationAccount.LastActivityDate = now;
                authenticationAccount.LastLoginDate = now;
                dataContext.SubmitChanges();

                return AuthenticationResult.OK;
            }
        }


        public void UpdateUserAccount(int accountId, UserAccountUpdateInfo info)
        {
            throw new NotImplementedException();
        }

        public UserAccountInfo GetUserAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(string userName, string existingPassword, string newPassword)
        {
            throw new NotImplementedException();
        }


        private UserAccountingDataContext CreateDataContext()
        {
            return new UserAccountingDataContext("connection string!");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Knightrunner.Library.UserAccounting
{
    [ServiceContract(Namespace = "Knightrunner.Library.UserAccounting")]
    public interface IUserAccountService
    {
        [OperationContract]
        void CreateUserAccount(UserAccountCreateInfo info);

        [OperationContract]
        AuthenticationResult Autenticate(string userName, string password);

        [OperationContract]
        void UpdateUserAccount(int accountId, UserAccountUpdateInfo info);

        [OperationContract]
        UserAccountInfo GetUserAccount(string userName);

        [OperationContract]
        void ChangePassword(string userName, string existingPassword, string newPassword);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.UserAccounting
{
    public class UserAccountUpdateInfo
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
    }
}

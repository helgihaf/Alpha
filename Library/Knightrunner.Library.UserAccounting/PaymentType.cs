using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.UserAccounting
{
    public enum PaymentType
    {
        None,
        CreditCard,
        DebetCard,
        PayPal,
        WebMoney
    }

    public enum PaymentBrand
    {
        None,
        Visa,
        MasterCard,
        AmericanExpress,
        Jcb
    }
}

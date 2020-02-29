using System;
using System.Collections.Generic;
using System.Text;

namespace CurbWrap.AppSettings
{
    public class regCreditCard : CreditCard
    {
        public regCreditCard()
        {
            base.isRegistering = true;
        }
    }
}

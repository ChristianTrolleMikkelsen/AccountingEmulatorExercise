using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneSubscriptionCalculator.Service_Calls
{
    public interface ICall
    {
        string SourcePhoneNumber { get; }
    }
}

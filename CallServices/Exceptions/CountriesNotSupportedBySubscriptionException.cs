using System;
using Core.ServiceCalls;

namespace CallServices.Exceptions
{
    public class CountriesNotSupportedBySubscriptionException : Exception
    {
        private readonly IServiceCall _call;

        public CountriesNotSupportedBySubscriptionException(IServiceCall call)
        {
            _call = call;
        }

        public override string ToString()
        {
            return string.Format("Your subscription do not support calls from {0} to {1}. ", _call.FromCountry,_call.ToCountry);
        }
    }
}

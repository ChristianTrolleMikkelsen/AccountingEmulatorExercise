using System;
using Core.ServiceCalls;

namespace CallServices.Exceptions
{
    public class ServiceNotSupportedBySubscriptionException : Exception
    {
        private readonly IServiceCall _call;

        public ServiceNotSupportedBySubscriptionException(IServiceCall call)
        {
            _call = call;
        }

        public override string ToString()
        {
            return string.Format("Your subscription do not support usage of {0}. ", _call.GetType().Name);
        }
    }
}

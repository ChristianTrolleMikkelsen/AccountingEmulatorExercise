using System;
using System.Linq;
using Core.ServiceCalls;
using SubscriptionServices;

namespace CallServices.Validator.Rules
{
    public class SubscriptionHasSupportForCall : IRule
    {
        private readonly IServiceSearch _serviceSearch;

        public SubscriptionHasSupportForCall(IServiceSearch serviceSearch)
        {
            _serviceSearch = serviceSearch;
        }

        public void Validate(IServiceCall call)
        {
            if (ServiceCallSupportedBySubscription(call.PhoneNumber, call.Type) == false)
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", (object) call.GetType().Name));
            }
        }

        private bool ServiceCallSupportedBySubscription(string phoneNumber, ServiceCallType callType)
        {
            return _serviceSearch.GetServicesBySubscription(phoneNumber)
                                        .Any(service => service.HasSupportForCallType(callType));
        }
    }
}

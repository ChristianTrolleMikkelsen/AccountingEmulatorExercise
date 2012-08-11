using System;
using System.Linq;
using ChargeServices;
using Core.ServiceCalls;

namespace CallServices.Validator.Rules
{
    public class SubscriptionHasSupportForCall : IRule
    {
        private readonly IServiceChargeSearch _serviceChargeSearch;

        public SubscriptionHasSupportForCall(IServiceChargeSearch serviceChargeSearch)
        {
            _serviceChargeSearch = serviceChargeSearch;
        }

        public void Validate(IServiceCall call)
        {
            if (ServiceCallSupportedBySubscription(call) == false)
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", (object) call.GetType().Name));
            }
        }

        private bool ServiceCallSupportedBySubscription(IServiceCall call)
        {
            return _serviceChargeSearch.GetServiceChargesBySubscripton(call.PhoneNumber)
                                            .Any(charge => charge.ServiceType == call.Type);
        }
    }
}

using System.Linq;
using CallServices.Exceptions;
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
                throw new ServiceNotSupportedBySubscriptionException(call);
            }
        }

        private bool ServiceCallSupportedBySubscription(IServiceCall call)
        {
            return _serviceChargeSearch.GetServiceChargesBySubscripton(call.PhoneNumber)
                                            .Any(charge => charge.ServiceType == call.Type);
        }
    }
}

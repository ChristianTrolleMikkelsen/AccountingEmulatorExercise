using System;
using Core.ServiceCalls;
using SubscriptionService;

namespace CallCentral.Validator.Rules
{
    public class SubscriptionHasSupportForCountriesInCall : IRule
    {
        private readonly ISubscriptionService _subscriptionService;
        
        public SubscriptionHasSupportForCountriesInCall(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public void Validate(IServiceCall call)
        {
            if (!_subscriptionService.IsCountriesSuppertedByAnyServicesIncludedInTheSubscription(call.PhoneNumber, call.FromCountry, call.ToCountry))
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", call.FromCountry, call.ToCountry));
            }
        }
    }
}

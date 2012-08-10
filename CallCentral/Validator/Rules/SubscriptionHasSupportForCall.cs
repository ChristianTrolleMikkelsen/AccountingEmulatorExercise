using System;
using Core.ServiceCalls;
using SubscriptionService;

namespace CallCentral.Validator.Rules
{
    public class SubscriptionHasSupportForCall : IRule
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionHasSupportForCall(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public void Validate(IServiceCall call)
        {
            if (!_subscriptionService.IsServiceCallSupportedBySubscription(call.PhoneNumber, call.Type))
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", call.GetType().Name));
            }
        }
    }
}

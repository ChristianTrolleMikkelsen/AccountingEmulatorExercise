using System;
using Core.ServiceCalls;
using SubscriptionService;

namespace CallCentral
{
    public interface ICallValidator
    {
        void ValidateCall(IServiceCall call);
    }

    public class CallValidator : ICallValidator
    {
        private readonly ISubscriptionService _subscriptionService;

        public CallValidator(ISubscriptionService subscriptionService )
        {
            _subscriptionService = subscriptionService;
        }

        public void ValidateCall(IServiceCall call)
        {
            CheckIfCallHasRequiredInformation(call);

            CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(call);

            CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(call);
        }

        private void CheckIfCallHasRequiredInformation(IServiceCall call)
        {
            if (call.IsValid() == false)
            {
                throw new Exception(string.Format("Your call was dimissed as it contained invalid information: {0},", call.ToString()));
            }
        }

        private void CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(IServiceCall call)
        {
            if (!_subscriptionService.IsServiceCallSupportedBySubscription(call.PhoneNumber, call.Type))
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", call.GetType().Name));
            }
        }

        private void CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(IServiceCall call)
        {
            if (!_subscriptionService.IsCountriesSuppertedByAnyServicesIncludedInTheSubscription(call.PhoneNumber,call.FromCountry,call.ToCountry))
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", call.FromCountry, call.ToCountry));
            }
        }
    }
}

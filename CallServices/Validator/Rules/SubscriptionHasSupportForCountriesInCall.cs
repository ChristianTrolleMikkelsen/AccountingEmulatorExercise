using System;
using System.Linq;
using Core.ServiceCalls;
using SubscriptionServices;

namespace CallServices.Validator.Rules
{
    public class SubscriptionHasSupportForCountriesInCall : IRule
    {
        private readonly IServiceChargeSearch _serviceChargeSearch;
        
        public SubscriptionHasSupportForCountriesInCall( IServiceChargeSearch serviceChargeSearch)
        {
            _serviceChargeSearch = serviceChargeSearch;
        }

        public void Validate(IServiceCall call)
        {
            if (CountriesAreSuppertedByAnyServicesIncludedInTheSubscription(call) == false)
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", call.FromCountry, call.ToCountry));
            }
        }

        private bool CountriesAreSuppertedByAnyServicesIncludedInTheSubscription(IServiceCall call)
        {
            var chargesWhichSupportTheCallType = _serviceChargeSearch.GetServiceChargesBySubscriptonAndCallType(call.PhoneNumber, call.Type);

            return chargesWhichSupportTheCallType.Any(charge => charge.Country == call.FromCountry)
                   && chargesWhichSupportTheCallType.Any(charge => charge.Country == call.ToCountry);
        }
    }
}

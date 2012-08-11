using System;
using System.Collections.Generic;
using System.Linq;
using Core.ServiceCalls;
using Core.ServiceCharges;
using SubscriptionServices;

namespace CallServices.Validator.Rules
{
    public class SubscriptionHasSupportForCountriesInCall : IRule
    {
        private readonly IServiceChargeSearch _serviceChargeSearch;
        
        public SubscriptionHasSupportForCountriesInCall(IServiceChargeSearch serviceChargeSearch)
        {
            _serviceChargeSearch = serviceChargeSearch;
        }

        public void Validate(IServiceCall call)
        {
            if (ThereIsSupportForBothContriesInCall(call) == false)
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", call.FromCountry, call.ToCountry));
            }
        }

        private bool ThereIsSupportForBothContriesInCall(IServiceCall call)
        {
            var chargesWhichSupportTheCallType = GetServiceChargesWhichSupportTheCall(call);

            return chargesWhichSupportTheCallType.Any(charge => charge.Country == call.FromCountry)
                   && chargesWhichSupportTheCallType.Any(charge => charge.Country == call.ToCountry);
        }

        private IEnumerable<IServiceCharge> GetServiceChargesWhichSupportTheCall(IServiceCall call)
        {
            return _serviceChargeSearch.GetServiceChargesBySubscriptonAndCallType(call.PhoneNumber)
                                          .Where(charge => charge.ServiceType == call.Type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ChargeServices;
using Core.ServiceCalls;
using Core.ServiceCharges;

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

            return    HasChargeWithSameCountry(chargesWhichSupportTheCallType, call.FromCountry)
                   && HasChargeWithSameCountry(chargesWhichSupportTheCallType, call.ToCountry);
        }

        private bool HasChargeWithSameCountry(IEnumerable<IServiceCharge> charges, string country)
        {
            return charges.Any(charge => HasSameCountry(charge, country));
        }

        private bool HasSameCountry(IServiceCharge charge, string country)
        {
            return charge.Country == country;
        }

        private IEnumerable<IServiceCharge> GetServiceChargesWhichSupportTheCall(IServiceCall call)
        {
            return _serviceChargeSearch.GetServiceChargesBySubscriptonAndCallType(call.PhoneNumber)
                                          .Where(charge => charge.ServiceType == call.Type).ToList();
        }
    }
}

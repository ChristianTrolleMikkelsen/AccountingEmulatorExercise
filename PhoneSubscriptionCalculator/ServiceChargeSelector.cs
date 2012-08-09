using System.Collections.Generic;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;

namespace PhoneSubscriptionCalculator
{
    public interface IServiceChargeSelector
    {
        IEnumerable<IServiceCharge> GetServiceChargesForServiceBasedOnCallSourceAndDestination(IServiceCall call);
    }

    public class ServiceChargeSelector : IServiceChargeSelector
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ILocalServiceChargeRepository _localServiceChargeRepository;
        private readonly IForeignServiceChargeRepository _foreignServiceChargeRepository;
        private ISubscription _subscription;

        public ServiceChargeSelector( ISubscriptionRepository subscriptionRepository,
                                                ILocalServiceChargeRepository localServiceChargeRepository,
                                                IForeignServiceChargeRepository foreignServiceChargeRepository )
        {
            _subscriptionRepository = subscriptionRepository;
            _localServiceChargeRepository = localServiceChargeRepository;
            _foreignServiceChargeRepository = foreignServiceChargeRepository;
        }

        public IEnumerable<IServiceCharge> GetServiceChargesForServiceBasedOnCallSourceAndDestination(IServiceCall call)
        {
            _subscription = _subscriptionRepository.GetSubscriptionForPhoneNumber(call.PhoneNumber);

            return (FindServiceChargesForLocalCall(call) ?? 
                    FindServiceChargesForForeignCall(call));
        }

        private IEnumerable<IServiceCharge> FindServiceChargesForLocalCall(IServiceCall call)
        {
            if (CallIsForeign(call) == false)
            {
                return _localServiceChargeRepository.GetServiceChargesForPhoneNumber(call.PhoneNumber);
            }
            return null;
        }

        private IEnumerable<IServiceCharge> FindServiceChargesForForeignCall(IServiceCall call)
        {
            if (CallIsForeign(call) == true)
            {
                var foreignIsoCode = GetForeignCountryIsoCode(call);
                return _foreignServiceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(foreignIsoCode, call.PhoneNumber);
            }
            return null;
        }

        private string GetForeignCountryIsoCode(IServiceCall call)
        {
            return (call.FromCountry != _subscription.LocalCountry ? call.FromCountry : call.ToCountry);
        }

        private bool CallIsForeign(IServiceCall call)
        {
            return (call.FromCountry != _subscription.LocalCountry ||
                    call.ToCountry != _subscription.LocalCountry);
        }
    }
}

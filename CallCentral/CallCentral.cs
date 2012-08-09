using System;
using System.Collections.Generic;
using System.Linq;
using CallCentral.Repositories;
using Core.Repositories;
using Core.ServiceCalls;

namespace CallCentral
{
    public interface ICallCentral
    {
        void RegisterACall(IServiceCall serviceCall);
        IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber);
    }

    public class CallCentral : ICallCentral
    {
        private readonly ICallRepository _callRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public CallCentral(ICallRepository callRepository, IServiceRepository serviceRepository, IServiceChargeRepository serviceChargeRepository)
        {
            _callRepository = callRepository;
            _serviceRepository = serviceRepository;
            _serviceChargeRepository = serviceChargeRepository;
        }

        public void RegisterACall(IServiceCall serviceCall)
        {
            CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(serviceCall);

            CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(serviceCall);

            _callRepository.RegisterACallForPhone(serviceCall);
        }

        private void CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(IServiceCall call)
        {
            if (HasServicesWhichSupportsCall(call) == false)
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", call.GetType().Name));
            }
        }

        private bool HasServicesWhichSupportsCall(IServiceCall call)
        {
            return _serviceRepository.GetServicesForPhoneNumber(call.PhoneNumber)
                                        .Any(service => service.HasSupportForCall(call));
        }

        private void CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(IServiceCall call)
        {
            if (   CountryIsSupported(call.FromCountry, call.PhoneNumber) == false
                || CountryIsSupported(call.ToCountry, call.PhoneNumber) == false
                )
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", call.FromCountry, call.ToCountry));
            }
        }

        private bool CountryIsSupported(string countryIsoCode, string phoneNumber)
        {
            return _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(countryIsoCode, phoneNumber).Any();
        }

        public IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }
}

using System;
using System.Linq;
using Core.Repositories;
using Core.ServiceCalls;

namespace CallCentral
{
    public interface ICallValidator
    {
        void ValidateCall(IServiceCall call);
    }

    public class CallValidator : ICallValidator
    {
        private IServiceRepository _serviceRepository;
        private IServiceChargeRepository _serviceChargeRepository;

        public CallValidator(IServiceRepository serviceRepository, IServiceChargeRepository serviceChargeRepository)
        {
            _serviceRepository = serviceRepository;
            _serviceChargeRepository = serviceChargeRepository;
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
            if (CountryIsSupported(call.FromCountry, call.PhoneNumber) == false
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
    }
}

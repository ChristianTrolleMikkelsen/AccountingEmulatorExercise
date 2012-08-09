using System;
using System.Collections.Generic;
using System.Linq;
using CallCentral.Repositories;
using Core;
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
        private readonly IServiceChargeSelector _serviceChargeSelector;

        public CallCentral(ICallRepository callRepository, IServiceRepository serviceRepository, IServiceChargeSelector serviceChargeSelector)
        {
            _callRepository = callRepository;
            _serviceRepository = serviceRepository;
            _serviceChargeSelector = serviceChargeSelector;
        }

        public void RegisterACall(IServiceCall serviceCall)
        {
            CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(serviceCall);

            CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(serviceCall);

            _callRepository.RegisterACallForPhone(serviceCall);
        }

        private void CheckIfCallIsAllowToUseTheSerivceAsDefinedByTheSubscription(IServiceCall serviceCall)
        {
            if (HasServicesWhichSupportsCall(serviceCall) == false)
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", serviceCall.GetType().Name));
            }
        }

        private bool HasServicesWhichSupportsCall(IServiceCall serviceCall)
        {
            return _serviceRepository.GetServicesForPhoneNumber(serviceCall.PhoneNumber)
                                        .Any(service => service.HasSupportForCall(serviceCall));
        }

        private void CheckIfCallIsWithinTheCountryRangeDefinedByTheSubscription(IServiceCall serviceCall)
        {
            if(_serviceChargeSelector.GetServiceChargesForServiceBasedOnCallSourceAndDestination(serviceCall).Any() == false)
            {
                throw new Exception(string.Format("Your subscription do not support calls from {0} to {1}. ", serviceCall.FromCountry, serviceCall.ToCountry));
            }
        }

        public IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }
}

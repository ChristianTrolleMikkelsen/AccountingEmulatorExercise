using System;
using System.Collections.Generic;
using System.Linq;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator
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

        public CallCentral(ICallRepository callRepository, IServiceRepository serviceRepository)
        {
            _callRepository = callRepository;
            _serviceRepository = serviceRepository;
        }

        public void RegisterACall(IServiceCall serviceCall)
        {
            CheckIfTheCallerHasASubscriptionWithTheNeededService(serviceCall);

            _callRepository.RegisterACallForPhone(serviceCall);
        }

        private void CheckIfTheCallerHasASubscriptionWithTheNeededService(IServiceCall serviceCall)
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

        public IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }
}

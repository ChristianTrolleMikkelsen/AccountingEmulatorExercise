using System;
using System.Collections.Generic;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator
{
    public interface ICallCentral
    {
        void RegisterACall(ICall call);
        IEnumerable<ICall> GetCallsMadeFromPhoneNumber(string phoneNumber);
    }

    public class CallCentral : ICallCentral
    {
        private readonly ICallRepository _callRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public CallCentral(ICallRepository callRepository, ISubscriptionRepository subscriptionRepository)
        {
            _callRepository = callRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public void RegisterACall(ICall call)
        {
            CheckIfTheCallerHasASubscriptionWithTheNeededService(call);

            _callRepository.RegisterACallForPhone(call);
        }

        private void CheckIfTheCallerHasASubscriptionWithTheNeededService(ICall call)
        {
            var subscription = _subscriptionRepository.GetSubscriptionForPhoneNumber(call.SourcePhoneNumber);

            if(subscription.HasServicesWhichSupportsCall(call) == false)
            {
                throw new Exception(string.Format("Your subscription do not support usage of {0}. ", call.GetType().Name));
            }
        }

        public IEnumerable<ICall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }
}

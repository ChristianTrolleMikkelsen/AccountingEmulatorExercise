using System.Collections.Generic;
using CallServices.Repositories;
using CallServices.Validator;
using Core.ServiceCalls;

namespace CallServices
{
   /* public interface ICallCentral
    {
        void RegisterACall(IServiceCall serviceCall);
        IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber);
    }

    public class CallCentral : ICallCentral
    {
        private readonly ICallValidator _validator;
        private readonly ICallRepository _callRepository;

        public CallCentral(ICallValidator validator, ICallRepository callRepository)
        {
            _validator = validator;
            _callRepository = callRepository;
        }

        public void RegisterACall(IServiceCall serviceCall)
        {
            _validator.ValidateCall(serviceCall);

            _callRepository.RegisterACallForPhone(serviceCall);
        }

        public IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }*/
}

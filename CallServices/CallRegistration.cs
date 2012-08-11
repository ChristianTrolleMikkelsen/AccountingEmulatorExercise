using CallServices.Repositories;
using CallServices.Validator;
using Core.ServiceCalls;

namespace CallServices
{
    public interface ICallRegistration
    {
        void RegisterACall(IServiceCall serviceCall);
    }

    public class CallRegistration : ICallRegistration
    {
        private readonly ICallValidator _validator;
        private readonly ICallRepository _callRepository;

        public CallRegistration(ICallValidator validator, ICallRepository callRepository)
        {
            _validator = validator;
            _callRepository = callRepository;
        }

        public void RegisterACall(IServiceCall serviceCall)
        {
            _validator.ValidateCall(serviceCall);

            _callRepository.RegisterACallForPhone(serviceCall);
        }
    }
}

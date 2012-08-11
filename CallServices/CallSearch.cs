using System.Collections.Generic;
using CallServices.Repositories;
using Core.ServiceCalls;

namespace CallServices
{
    public interface ICallSearch
    {
        IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber);
    }

    public class CallSearch : ICallSearch
    {
        private readonly ICallRepository _callRepository;

        public CallSearch(ICallRepository callRepository)
        {
            _callRepository = callRepository;
        }

        public IEnumerable<IServiceCall> GetCallsMadeFromPhoneNumber(string phoneNumber)
        {
            return _callRepository.GetCallsMadeByPhone(phoneNumber);
        }
    }
}

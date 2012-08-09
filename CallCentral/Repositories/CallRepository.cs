using System.Collections.Generic;
using System.Linq;
using Core.ServiceCalls;

namespace CallCentral.Repositories
{
    public interface ICallRepository
    {
        IEnumerable<IServiceCall> GetCallsMadeByPhone(string phoneNumber);
        void RegisterACallForPhone(IServiceCall serviceCall);
    }

    public class CallRepository : ICallRepository
    {
        private List<IServiceCall> _calls;

        public CallRepository()
        {
            _calls = new List<IServiceCall>();
        }

        public IEnumerable<IServiceCall> GetCallsMadeByPhone(string phoneNumber)
        {
            return _calls.Where(call => call.PhoneNumber == phoneNumber)
                            .ToList();
        }

        public void RegisterACallForPhone(IServiceCall serviceCall)
        {
            _calls.Add(serviceCall);
        }
    }
}

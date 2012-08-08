using System.Collections.Generic;
using System.Linq;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Repositories
{
    public interface ICallRepository
    {
        IEnumerable<ICall> GetCallsMadeByPhone(string phoneNumber);
        void RegisterACallForPhone(ICall call);
    }

    public class CallRepository : ICallRepository
    {
        private List<ICall> _calls;

        public CallRepository()
        {
            _calls = new List<ICall>();
        }

        public IEnumerable<ICall> GetCallsMadeByPhone(string phoneNumber)
        {
            return _calls.Where(call => call.SourcePhoneNumber == phoneNumber)
                            .ToList();
        }

        public void RegisterACallForPhone(ICall call)
        {
            _calls.Add(call);
        }
    }
}

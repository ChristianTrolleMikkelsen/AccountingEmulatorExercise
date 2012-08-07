using System;
using System.Collections.Generic;
using PhoneSubscriptionCalculator.Models;

namespace PhoneSubscriptionCalculator.Repositories
{
    public interface IPhoneCallRepository
    {
        IEnumerable<ICall> GetCallsForPhone(string phoneNumber);
        void RegisterACallForPhone(ICall call);
    }

    public class PhoneCallRepository : IPhoneCallRepository
    {
        public IEnumerable<ICall> GetCallsForPhone(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void RegisterACallForPhone(ICall call)
        {
            throw new NotImplementedException();
        }
    }
}

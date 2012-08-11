using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace SubscriptionServices.Repositories
{
    public interface ISubscriptionRepository
    {
        ISubscription GetSubscriptionForPhoneNumber(string phoneNumber);
        void SaveSubscription(ISubscription subscription);
    }

    public class SubscriptionRepository : ISubscriptionRepository
    {
        private List<ISubscription> _subscriptions;

        public SubscriptionRepository()
        {
            _subscriptions = new List<ISubscription>();
        }

        public void SaveSubscription(ISubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public ISubscription GetSubscriptionForPhoneNumber(string phoneNumber)
        {
            return _subscriptions.First(sub => sub.PhoneNumber == phoneNumber);
        }
    }
}

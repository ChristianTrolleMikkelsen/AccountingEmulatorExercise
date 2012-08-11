using Core.Models;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface ISubscriptionSearch
    {
        ISubscription GetSubscription(string phoneNumber);
    }

    public class SubscriptionSearch : ISubscriptionSearch
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionSearch(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public ISubscription GetSubscription(string phoneNumber)
        {
            return _subscriptionRepository.GetSubscriptionForPhoneNumber(phoneNumber);
        }
    }
}

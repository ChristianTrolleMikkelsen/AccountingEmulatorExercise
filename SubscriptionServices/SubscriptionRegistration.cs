using Core.Models;
using SubscriptionServices.Models;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface ISubscriptionRegistration
    {
        ISubscription CreateSubscription(ICustomer customer, string phoneNumber, string subscriptionCountryIsoCode);
    }

    public class SubscriptionRegistration : ISubscriptionRegistration
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionRegistration(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public ISubscription CreateSubscription(ICustomer customer, string phoneNumber, string subscriptionCountryIsoCode)
        {
            var subscription = new Subscription(customer, phoneNumber, subscriptionCountryIsoCode);
            _subscriptionRepository.SaveSubscription(subscription);
            return subscription;
        }
    }
}

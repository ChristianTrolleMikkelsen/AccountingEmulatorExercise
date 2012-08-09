using Core.Models;

namespace TestHelpers
{
    public class SubscriptionHelper
    {
        public static ISubscription CreateSubscriptionWithDefaultCustomer(string phoneNumber, string countryIsoCode = "DK")
        {
            return new Subscription(new Customer("John Doe"),phoneNumber, countryIsoCode );
        }

        public static ISubscription CreateSubscriptionWithDefaultCustomer(ICustomer customer, string phoneNumber, string countryIsoCode = "DK")
        {
            return new Subscription(customer, phoneNumber, countryIsoCode);
        }
    }
}

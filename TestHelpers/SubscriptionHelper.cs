using Core.Models;
using SubscriptionServices;


namespace TestHelpers
{
    public class SubscriptionHelper
    {
        public static ISubscription CreateSubscriptionWithDefaultCustomer(ISubscriptionRegistration subscriptionRegistration, ICustomerRegistration customerRegistration, string phoneNumber, string countryIsoCode, CustomerStatus status)
        {
            var customer = customerRegistration.CreateCustomer("John Doe", status);

            return subscriptionRegistration.CreateSubscription(customer,phoneNumber, countryIsoCode);
        }
    }
}

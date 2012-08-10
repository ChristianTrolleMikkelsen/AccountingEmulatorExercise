using Core.Models;
using SubscriptionService;

namespace TestHelpers
{
    public class SubscriptionHelper
    {
        public static ISubscription CreateSubscriptionWithDefaultCustomer(ISubscriptionService service, string phoneNumber, string countryIsoCode, CustomerStatus status)
        {
            var customer = service.CreateCustomer("John Doe", status);

            return service.CreateSubscription(customer,phoneNumber, countryIsoCode);
        }
    }
}

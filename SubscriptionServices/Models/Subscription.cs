using Core.Models;

namespace SubscriptionServices.Models
{
    internal class Subscription : ISubscription
    {
        public ICustomer Customer { get; private set; }
        public string PhoneNumber { get; private set; }
        public string LocalCountry { get; private set; }

        public Subscription(ICustomer customer, string phoneNumber, string countryIsoCode = "DK")
        {
            Customer = customer;
            PhoneNumber = phoneNumber;
            LocalCountry = countryIsoCode;
        }
    }
}

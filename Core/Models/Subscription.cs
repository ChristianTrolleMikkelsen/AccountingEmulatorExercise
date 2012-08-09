namespace Core.Models
{
    public interface ISubscription
    {
        ICustomer Customer { get; }
        string PhoneNumber { get; }
        string LocalCountry { get; }
    }

    public class Subscription : ISubscription
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

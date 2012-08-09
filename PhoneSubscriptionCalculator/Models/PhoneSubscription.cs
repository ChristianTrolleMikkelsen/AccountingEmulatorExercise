namespace PhoneSubscriptionCalculator.Models
{
    public interface ISubscription
    {
        string PhoneNumber { get; }
        string LocalCountry { get; }
    }

    public class Subscription : ISubscription
    {
        public string PhoneNumber { get; private set; }
        public string LocalCountry { get; private set; }

        public Subscription(string phoneNumber, string countryIsoCode = "DK")
        {
            PhoneNumber = phoneNumber;
            LocalCountry = countryIsoCode;
        }
    }
}

namespace PhoneSubscriptionCalculator.Models
{
    public interface ISubscription
    {
        string PhoneNumber { get; }
        Country LocalCountry { get; }
    }

    public class Subscription : ISubscription
    {
        public string PhoneNumber { get; private set; }
        public Country LocalCountry { get; private set; }

        public Subscription(string phoneNumber, string countryIsoCode = "DK")
        {
            PhoneNumber = phoneNumber;
            LocalCountry = new Country(countryIsoCode);
        }
    }
}

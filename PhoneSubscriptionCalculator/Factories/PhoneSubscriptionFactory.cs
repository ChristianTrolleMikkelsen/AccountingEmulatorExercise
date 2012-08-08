using PhoneSubscriptionCalculator.Models;

namespace PhoneSubscriptionCalculator.Factories
{
    public interface IPhoneSubscriptionFactory
    {
        ISubscription CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(string phoneNumber, string localCountryIsoCode = "DK");
    }

    public class PhoneSubscriptionFactory : IPhoneSubscriptionFactory
    {
        public ISubscription CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(string phoneNumber, string localCountryIsoCode)
        {
            return new Subscription(phoneNumber, localCountryIsoCode);
        }
    }
}

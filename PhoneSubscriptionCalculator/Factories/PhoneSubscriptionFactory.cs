using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;

namespace PhoneSubscriptionCalculator.Factories
{
    public interface IPhoneSubscriptionFactory
    {
        ISubscription CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(string phoneNumber, string localCountryIsoCode = "DK");
    }

    public class PhoneSubscriptionFactory : IPhoneSubscriptionFactory
    {
        private readonly IServiceRepository _serviceRepository;

        public PhoneSubscriptionFactory(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public ISubscription CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(string phoneNumber, string localCountryIsoCode)
        {
            return new Subscription(_serviceRepository, phoneNumber, localCountryIsoCode);
        }
    }
}

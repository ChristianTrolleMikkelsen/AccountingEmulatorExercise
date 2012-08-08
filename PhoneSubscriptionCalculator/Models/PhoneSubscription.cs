using System.Collections.Generic;
using System.Linq;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Services;

namespace PhoneSubscriptionCalculator.Models
{
    public interface ISubscription
    {
        void AddService(IService service);
        IEnumerable<IService> GetServices();
        bool HasServicesWhichSupportsCall(ICall call);

        string PhoneNumber { get; }
        Country LocalCountry { get; }
    }

    public class Subscription : ISubscription
    {
        private readonly IServiceRepository _serviceRepository;
        public string PhoneNumber { get; private set; }
        public Country LocalCountry { get; private set; }

        public Subscription(IServiceRepository serviceRepository, string phoneNumber, string countryIsoCode = "DK")
        {
            _serviceRepository = serviceRepository;
            PhoneNumber = phoneNumber;
            LocalCountry = new Country(countryIsoCode);
        }

        public IEnumerable<IService> GetServices()
        {
            return _serviceRepository.GetServicesForPhoneNumber(PhoneNumber);
        }

        public void AddService(IService service)
        {
            if (ListDoNotContainAServiceOfSameType(service))
            {
                _serviceRepository.SaveService(service);
            }
        }

        private bool ListDoNotContainAServiceOfSameType(IService service)
        {
            return !_serviceRepository.GetServicesForPhoneNumber(PhoneNumber)
                                        .Any(listedService => listedService.GetType() == service.GetType());
        }

        public bool HasServicesWhichSupportsCall(ICall call)
        {
            return _serviceRepository.GetServicesForPhoneNumber(PhoneNumber)
                .Any(service => service.HasSupportForCall(call));
        }
    }
}

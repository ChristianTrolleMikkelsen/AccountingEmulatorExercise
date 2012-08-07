using System.Collections.Generic;
using System.Linq;

namespace PhoneSubscriptionCalculator.Models
{
    public interface IPhoneSubscription
    {
        void AddService(IService service);
        IEnumerable<IService> GetServices();

        IPhoneSubscription RegisterACall(ICall call);

        string PhoneNumber { get; }
        Country LocalCountry { get; }
        IEnumerable<ICall> GetCalls();
    }

    public class PhoneSubscription : IPhoneSubscription
    {
        private List<IService> _services;
        private List<ICall> _calls;

        public string PhoneNumber { get; private set; }

        public Country LocalCountry { get; private set; }

        public PhoneSubscription(string phoneNumber)
        {
            Initialize(phoneNumber, "DKK");//TODO: In better version remove magic strings
        }

        public PhoneSubscription(string phoneNumber, string countryIsoCode)
        {
            Initialize(phoneNumber, countryIsoCode);
        }

        private void Initialize(string phoneNumber, string countryIsoCode)
        {
            PhoneNumber = phoneNumber;
            _services = new List<IService>();
            _calls = new List<ICall>();
            LocalCountry = new Country(countryIsoCode);
        }

        public IEnumerable<IService> GetServices()
        {
            return _services;
        }

        public void AddService(IService service)
        {
            if (ListDoNotContainAServiceOfSameType(_services, service))
            {
                _services.Add(service);
            }
        }

        private bool ListDoNotContainAServiceOfSameType(IEnumerable<IService> list, IService service)
        {
            return !list.Any(listedService => listedService.GetType() == service.GetType());
        }

        public IPhoneSubscription RegisterACall(ICall call)
        {
            _calls.Add(call);
            return this;
        }

        public IEnumerable<ICall> GetCalls()
        {
            return _calls;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using SubscriptionService.Models;
using SubscriptionService.Repositories;
using IService = SubscriptionService.Services.IService;

namespace SubscriptionService
{
    public interface ISubscriptionService
    {
        ICustomer CreateCustomer(string name, CustomerStatus status);
        
        ISubscription CreateSubscription(ICustomer customer, string phoneNumber, string subscriptionCountryIsoCode);

        void AddServiceToSubscription(IService service);
        void AddServiceChargeToSubscription(IServiceCharge serviceCharge);

        bool IsServiceCallSupportedBySubscription(string phoneNumber, ServiceCallType callType);
        bool IsCountriesSuppertedByAnyServicesIncludedInTheSubscription(string phoneNumber, string fromCountry, string toCountry);

        IEnumerable<IServiceCharge> GetServiceChargesSupportedBy(string phoneNumber, ServiceCallType callType);
        ISubscription GetSubscription(string phoneNumber);
    }

    public class SubscriptionService : ISubscriptionService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceChargeRepository _serviceChargeRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService( IServiceRepository serviceRepository, 
                                    IServiceChargeRepository serviceChargeRepository,
                                    ISubscriptionRepository subscriptionRepository)
        {
            _serviceRepository = serviceRepository;
            _serviceChargeRepository = serviceChargeRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public void AddServiceToSubscription(IService service)
        {
            _serviceRepository.SaveService(service);
        }

        public void AddServiceChargeToSubscription(IServiceCharge serviceCharge)
        {
            _serviceChargeRepository.SaveServiceCharge(serviceCharge);
        }

        public ICustomer CreateCustomer(string name, CustomerStatus status)
        {
            return new Customer(name, status);
        }

        public ISubscription CreateSubscription(ICustomer customer, string phoneNumber, string subscriptionCountryIsoCode)
        {
            _subscriptionRepository.SaveSubscription(new Subscription(customer,phoneNumber,subscriptionCountryIsoCode));
            return _subscriptionRepository.GetSubscriptionForPhoneNumber(phoneNumber);
        }

        public bool IsServiceCallSupportedBySubscription(string phoneNumber, ServiceCallType callType)
        {
            return _serviceRepository.GetServicesForPhoneNumber(phoneNumber)
                                        .Any(service => service.HasSupportForCallType(callType));
        }

        public bool IsCountriesSuppertedByAnyServicesIncludedInTheSubscription(string phoneNumber, string fromCountry, string toCountry)
        {
            return _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(fromCountry, phoneNumber).Any()
                && _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(toCountry, phoneNumber).Any();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesSupportedBy(string phoneNumber, ServiceCallType callType)
        {
            var servicesWithSupportForCallType = GetServicesSupportedByCallType(phoneNumber, callType);

            return _serviceChargeRepository.GetServiceChargesByPhoneNumber(phoneNumber)
                                                .Where(charge => servicesWithSupportForCallType
                                                                     .Any(service => service.Type == charge.ServiceType)).ToList();
        }

        public ISubscription GetSubscription(string phoneNumber)
        {
            return _subscriptionRepository.GetSubscriptionForPhoneNumber(phoneNumber);
        }

        private IEnumerable<IService>  GetServicesSupportedByCallType(string phoneNumber, ServiceCallType callType)
        {
            return _serviceRepository.GetServicesForPhoneNumber(phoneNumber)
                                        .Where(service => service.HasSupportForCallType(callType))
                                            .ToList();
        }
    }
}

using System.Collections.Generic;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface IServiceSearch
    {
        IEnumerable<IService> GetServicesBySubscription(string phoneNumber);
    }

    public class ServiceSearch : IServiceSearch
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceSearch(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IEnumerable<IService> GetServicesBySubscription(string phoneNumber)
        {
            return _serviceRepository.GetServicesForPhoneNumber(phoneNumber);
        }
    }
}

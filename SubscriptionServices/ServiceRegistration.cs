using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface IServiceRegistration
    {
        void AddServiceToSubscription(IService service);
    }

    public class ServiceRegistration : IServiceRegistration
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceRegistration(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void AddServiceToSubscription(IService service)
        {
            _serviceRepository.SaveService(service);
        }
    }
}

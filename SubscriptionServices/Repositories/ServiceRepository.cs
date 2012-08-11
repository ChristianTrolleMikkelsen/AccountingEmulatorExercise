using System.Collections.Generic;
using System.Linq;

namespace SubscriptionServices.Repositories
{
    public interface IServiceRepository
    {
        IEnumerable<IService> GetServicesForPhoneNumber(string phoneNumber);
        void SaveService(IService services);
    }

    public class ServiceRepository : IServiceRepository
    {
        private List<IService> _services;

        public ServiceRepository()
        {
            _services = new List<IService>();
        }

        public IEnumerable<IService> GetServicesForPhoneNumber(string phoneNumber)
        {
            return _services.Where(service => service.PhoneNumber == phoneNumber)
                                .ToList();
        }

        public void SaveService(IService services)
        {
            _services.Add(services);
        }
    }
}

using Core.ServiceCharges;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface IServiceChargeRegistration
    {
        void AddServiceChargeToSubscription(IServiceCharge serviceCharge);
    }

    public class ServiceChargeRegistration : IServiceChargeRegistration
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public ServiceChargeRegistration(IServiceChargeRepository serviceChargeRepository)
        {
            _serviceChargeRepository = serviceChargeRepository;
        }

        public void AddServiceChargeToSubscription(IServiceCharge serviceCharge)
        {
            _serviceChargeRepository.SaveServiceCharge(serviceCharge);
        }
    }
}

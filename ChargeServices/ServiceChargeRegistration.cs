using ChargeServices.Repositories;
using Core.ServiceCharges;

namespace ChargeServices
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

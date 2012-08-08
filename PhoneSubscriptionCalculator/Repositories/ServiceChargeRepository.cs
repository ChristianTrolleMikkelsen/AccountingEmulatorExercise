using System.Collections.Generic;
using System.Linq;
using PhoneSubscriptionCalculator.Service_Charges;
using PhoneSubscriptionCalculator.Services;

namespace PhoneSubscriptionCalculator.Repositories
{
    public interface IServiceChargeRepository
    {
        IEnumerable<IServiceCharge> GetServiceChargesForPhoneNumberAndService(string phoneNumber, IService service);
        void SaveServiceCharge(IServiceCharge serviceCharge);
    }

    public class ServiceChargeRepository : IServiceChargeRepository
    {
        private readonly List<IServiceCharge> _serviceCharges;
        
        public ServiceChargeRepository()
        {
            _serviceCharges = new List<IServiceCharge>();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesForPhoneNumberAndService(string phoneNumber, IService service)
        {
            return _serviceCharges.Where(charge => charge.PhoneNumber == phoneNumber).ToList();
        }

        public void SaveServiceCharge(IServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge);
        }
    }
}

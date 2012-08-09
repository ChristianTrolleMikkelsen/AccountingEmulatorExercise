using System.Collections.Generic;
using System.Linq;
using Core.ServiceCharges;

namespace Core.Repositories
{
    public interface ILocalServiceChargeRepository
    {
        IEnumerable<IServiceCharge> GetServiceChargesForPhoneNumber(string phoneNumber);
        void SaveServiceCharge(IServiceCharge serviceCharge);
    }

    public class LocalServiceChargeRepository : ILocalServiceChargeRepository
    {
        private readonly List<IServiceCharge> _serviceCharges;
        
        public LocalServiceChargeRepository()
        {
            _serviceCharges = new List<IServiceCharge>();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesForPhoneNumber(string phoneNumber)
        {
            return _serviceCharges.Where(charge => charge.PhoneNumber == phoneNumber).ToList();
        }

        public void SaveServiceCharge(IServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge);
        }
    }
}

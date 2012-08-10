using System.Collections.Generic;
using System.Linq;
using Core.ServiceCharges;

namespace SubscriptionService.Repositories
{
    public interface IServiceChargeRepository
    {
        IEnumerable<IServiceCharge> GetServiceChargesByPhoneNumber(string phoneNumber);
        IEnumerable<IServiceCharge> GetServiceChargesByCountryAndPhoneNumber(string countryIsoCode, string phoneNumber);
        void SaveServiceCharge(IServiceCharge serviceCharge);
    }

    public class ServiceChargeRepository : IServiceChargeRepository
    {
        private readonly List<IServiceCharge> _serviceCharges;

        public ServiceChargeRepository()
        {
            _serviceCharges = new List<IServiceCharge>();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesByPhoneNumber(string phoneNumber)
        {
            return _serviceCharges.Where(charge => charge.PhoneNumber == phoneNumber).ToList();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesByCountryAndPhoneNumber(string countryIsoCode, string phoneNumber)
        {
            return _serviceCharges.Where(charge => charge.PhoneNumber == phoneNumber)
                                        .Where(charge => charge.Country == countryIsoCode).ToList();
        }

        public void SaveServiceCharge(IServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge);
        }
    }
}

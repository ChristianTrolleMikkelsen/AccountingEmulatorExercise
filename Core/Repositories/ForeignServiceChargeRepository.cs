using System.Collections.Generic;
using System.Linq;
using Core.ServiceCharges;

namespace Core.Repositories
{
    public interface IForeignServiceChargeRepository
    {
        IEnumerable<IServiceCharge> GetServiceChargesByCountryAndPhoneNumber(string countryIsoCode, string phoneNumber);
        void SaveServiceCharge(string countryIsoCode, IServiceCharge serviceCharge);
    }

    public class ForeignServiceChargeRepository : IForeignServiceChargeRepository
    {
        private readonly Dictionary<string, List<IServiceCharge>> _serviceCharges;
        
        public ForeignServiceChargeRepository()
        {
            _serviceCharges = new Dictionary<string, List<IServiceCharge>>();
        }

        public IEnumerable<IServiceCharge> GetServiceChargesByCountryAndPhoneNumber(string countryIsoCode, string phoneNumber)
        {
            if (_serviceCharges.ContainsKey(countryIsoCode))
            {
                return _serviceCharges.First(entry => entry.Key == countryIsoCode).Value
                                        .Where(charge => charge.PhoneNumber == phoneNumber).ToList();
            }
            return new List<IServiceCharge>();
        }

        public void SaveServiceCharge(string countryIsoCode, IServiceCharge serviceCharge)
        {
            if(_serviceCharges.ContainsKey(countryIsoCode))
            {
                _serviceCharges[countryIsoCode].Add(serviceCharge);
            }
            else
            {
                _serviceCharges.Add(countryIsoCode, new List<IServiceCharge> { serviceCharge });
            }
        }
    }
}

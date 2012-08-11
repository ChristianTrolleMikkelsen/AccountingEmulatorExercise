using System.Collections.Generic;
using System.Linq;
using ChargeServices.Repositories;
using Core.ServiceCharges;

namespace ChargeServices
{
    public interface IServiceChargeSearch
    {
        IEnumerable<IServiceCharge> GetServiceChargesBySubscriptonAndCallType(string phoneNumber);
    }

    public class ServiceChargeSearch : IServiceChargeSearch
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public ServiceChargeSearch(IServiceChargeRepository serviceChargeRepository)
        {
            _serviceChargeRepository = serviceChargeRepository;
        }

        public IEnumerable<IServiceCharge> GetServiceChargesBySubscriptonAndCallType(string phoneNumber)
        {
            return _serviceChargeRepository.GetServiceChargesByPhoneNumber(phoneNumber).ToList();
        }
    }
}

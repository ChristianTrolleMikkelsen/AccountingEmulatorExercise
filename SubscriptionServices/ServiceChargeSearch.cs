using System.Collections.Generic;
using System.Linq;
using Core.ServiceCalls;
using Core.ServiceCharges;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public interface IServiceChargeSearch
    {
        IEnumerable<IServiceCharge> GetServiceChargesBySubscriptonAndCallType(string phoneNumber, ServiceCallType callType);
    }

    public class ServiceChargeSearch : IServiceChargeSearch
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;
        private readonly IServiceSearch _serviceSearch;

        public ServiceChargeSearch(IServiceChargeRepository serviceChargeRepository, IServiceSearch serviceSearch)
        {
            _serviceChargeRepository = serviceChargeRepository;
            _serviceSearch = serviceSearch;
        }

        public IEnumerable<IServiceCharge> GetServiceChargesBySubscriptonAndCallType(string phoneNumber, ServiceCallType callType)
        {
            var servicesWithSupportForCallType = GetServicesSupportedByCallType(phoneNumber, callType);

            return _serviceChargeRepository.GetServiceChargesByPhoneNumber(phoneNumber)
                    .Where(charge => servicesWithSupportForCallType
                        .Any(service => service.Type == charge.ServiceType)).ToList();
        }

        private IEnumerable<IService> GetServicesSupportedByCallType(string phoneNumber, ServiceCallType callType)
        {
            return _serviceSearch.GetServicesBySubscription(phoneNumber)
                                    .Where(service => service.HasSupportForCallType(callType))
                                            .ToList();
        }
    }
}

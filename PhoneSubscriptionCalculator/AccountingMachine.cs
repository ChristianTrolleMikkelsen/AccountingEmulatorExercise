using System;
using System.Linq;
using MoreLinq;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;
using PhoneSubscriptionCalculator.Services;

namespace PhoneSubscriptionCalculator
{
    public interface IAccountingMachine
    {
        void GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class AccountingMachine : IAccountingMachine
    {
        private readonly ICallCentral _callCentral;
        private readonly ICallRepository _callRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceChargeRepository _serviceChargeRepository;
        private readonly IRecordRepository _recordRepository;

        public AccountingMachine(   ICallCentral callCentral,
                                    ICallRepository callRepository,
                                    ISubscriptionRepository subscriptionRepository, 
                                    IServiceRepository serviceRepository, 
                                    IServiceChargeRepository serviceChargeRepository,
                                    IRecordRepository recordRepository)
        {
            _callCentral = callCentral;
            _callRepository = callRepository;
            _subscriptionRepository = subscriptionRepository;
            _serviceRepository = serviceRepository;
            _serviceChargeRepository = serviceChargeRepository;
            _recordRepository = recordRepository;
        }

        public void GenerateBillForPhoneNumber(string phoneNumber)
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(phoneNumber);

            calls.ForEach(ProcessCall);
        }

        private void ProcessCall(IServiceCall call)
        {
            var services = _serviceRepository.GetServicesForPhoneNumber(call.PhoneNumber);

            var servicesWhichSupportTheCall = services.Where(service => service.HasSupportForCall(call)).ToList();

            servicesWhichSupportTheCall.ForEach(service => CalculateServiceCharge(service, call));
        }

        private void CalculateServiceCharge(IService service, IServiceCall call)
        {
            var charges = _serviceChargeRepository.GetServiceChargesForPhoneNumberAndService(call.PhoneNumber, service);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private void GenerateRecord(IServiceCharge charge, IServiceCall call)
        {
            _recordRepository.SaveRecord(charge.GenerateBill(call));
        }
    }
}

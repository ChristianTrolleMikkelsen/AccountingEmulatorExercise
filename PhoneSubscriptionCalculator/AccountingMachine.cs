using System.Linq;
using MoreLinq;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;

namespace PhoneSubscriptionCalculator
{
    public interface IAccountingMachine
    {
        void GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class AccountingMachine : IAccountingMachine
    {
        private readonly ICallCentral _callCentral;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IServiceChargeSelector _serviceChargeSelector;

        public AccountingMachine(   ICallCentral callCentral,
                                    IServiceRepository serviceRepository, 
                                    IRecordRepository recordRepository,
                                    IServiceChargeSelector serviceChargeSelector)
        {
            _callCentral = callCentral;
            _serviceRepository = serviceRepository;
            _recordRepository = recordRepository;
            _serviceChargeSelector = serviceChargeSelector;
        }

        public void GenerateBillForPhoneNumber(string phoneNumber)
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(phoneNumber);

            calls.ForEach(GenerateRecordsForBill);

            GenerateBill(phoneNumber);
        }

        private void GenerateRecordsForBill(IServiceCall call)
        {
            var services = _serviceRepository.GetServicesForPhoneNumber(call.PhoneNumber);

            var servicesWhichSupportTheCall = services.Where(service => service.HasSupportForCall(call)).ToList();

            servicesWhichSupportTheCall.ForEach(service => CalculateServiceCharge(call));
        }

        private void CalculateServiceCharge(IServiceCall call)
        {
            var charges = _serviceChargeSelector.GetServiceChargesForServiceBasedOnCallSourceAndDestination(call);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private void GenerateRecord(IServiceCharge charge, IServiceCall call)
        {
            var newRecord = new Record(call.PhoneNumber,
                                       call.ToString(),
                                       charge.CalculateCharge(call));

            _recordRepository.SaveRecord(newRecord);
        }

        private void GenerateBill(string phoneNumber)
        {
            //TODO
        }
    }
}

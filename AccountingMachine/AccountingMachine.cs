using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core;
using Core.Repositories;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using MoreLinq;

namespace AccountingMachine
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
            var servicesWhichSupportTheCall = FindServicesWhichSupportThisTypeOfCall(call);

            servicesWhichSupportTheCall.ForEach(service => CalculateServiceCharge(call));
        }

        private IEnumerable<IService> FindServicesWhichSupportThisTypeOfCall(IServiceCall call)
        {
            return _serviceRepository.GetServicesForPhoneNumber(call.PhoneNumber)
                                                .Where(service => service.HasSupportForCall(call)).ToList();
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

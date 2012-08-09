using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
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
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public AccountingMachine(   ICallCentral callCentral,
                                    IServiceRepository serviceRepository, 
                                    IRecordRepository recordRepository,
                                    IServiceChargeRepository serviceChargeRepository)
        {
            _callCentral = callCentral;
            _serviceRepository = serviceRepository;
            _recordRepository = recordRepository;
            _serviceChargeRepository = serviceChargeRepository;
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
            var charges = GetServiceChargesForCall(call);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private IEnumerable<IServiceCharge> GetServiceChargesForCall(IServiceCall call)
        {
            var fromCharges = _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(call.FromCountry, call.PhoneNumber);
            var toCharges = _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(call.ToCountry, call.PhoneNumber);

            return fromCharges.Union(toCharges).Distinct();
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

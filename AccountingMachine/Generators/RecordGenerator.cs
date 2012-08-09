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

namespace AccountingMachine.Generators
{
    public interface IRecordGenerator
    {
        void GenerateAccountingRecordsForPhoneNumber(string phoneNumber);
    }

    public class RecordGenerator : IRecordGenerator
    {
        private readonly ICallCentral _callCentral;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public RecordGenerator(ICallCentral callCentral,
                                    IServiceRepository serviceRepository, 
                                    IRecordRepository recordRepository,
                                    IServiceChargeRepository serviceChargeRepository)
        {
            _callCentral = callCentral;
            _serviceRepository = serviceRepository;
            _recordRepository = recordRepository;
            _serviceChargeRepository = serviceChargeRepository;
        }


        public void GenerateAccountingRecordsForPhoneNumber(string phoneNumber)
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(phoneNumber);

            calls.ForEach(GenerateRecords);
        }

        private void GenerateRecords(IServiceCall call)
        {
            var servicesWhichSupportTheCall = FindServicesWhichSupportThisTypeOfCall(call);

            servicesWhichSupportTheCall.ForEach(service => CalculateRecordForEachServiceCharge(service, call));
        }

        private IEnumerable<IService> FindServicesWhichSupportThisTypeOfCall(IServiceCall call)
        {
            return _serviceRepository.GetServicesForPhoneNumber(call.PhoneNumber)
                                                .Where(service => service.HasSupportForCall(call)).ToList();
        }

        private void CalculateRecordForEachServiceCharge(IService service, IServiceCall call)
        {
            var charges = GetServiceChargesForCall(service,call);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private IEnumerable<IServiceCharge> GetServiceChargesForCall(IService service, IServiceCall call)
        {
            var fromCharges = _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(call.FromCountry, call.PhoneNumber);
            var toCharges = _serviceChargeRepository.GetServiceChargesByCountryAndPhoneNumber(call.ToCountry, call.PhoneNumber);

            return fromCharges.Union(toCharges)
                                .Where(charge => charge.ServiceType == service.GetType())
                                    .Distinct();
        }

        private void GenerateRecord(IServiceCharge charge, IServiceCall call)
        {
            var newRecord = new Record(call.PhoneNumber,
                                       call.GetStartTime(),
                                       charge.Description,
                                       call.ToString(),
                                       charge.CalculateCharge(call));

            _recordRepository.SaveRecord(newRecord);
        }
    }
}

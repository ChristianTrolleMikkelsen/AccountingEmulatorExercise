using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallServices;
using Core.ServiceCalls;
using Core.ServiceCharges;
using MoreLinq;
using SubscriptionServices;

namespace AccountingMachine.Generators
{
    public interface IRecordGenerator
    {
        void GenerateAccountingRecordsForPhoneNumber(string phoneNumber);
    }

    public class RecordGenerator : IRecordGenerator
    {
        private readonly ICallSearch _callSearch;
        private readonly IServiceChargeSearch _serviceChargeSearch;
        private readonly IRecordRepository _recordRepository;

        public RecordGenerator(ICallSearch callSearch, IServiceChargeSearch serviceChargeSearch, IRecordRepository recordRepository)
        {
            _callSearch = callSearch;
            _serviceChargeSearch = serviceChargeSearch;
            _recordRepository = recordRepository;
        }

        public void GenerateAccountingRecordsForPhoneNumber(string phoneNumber)
        {
            var calls = _callSearch.GetCallsMadeFromPhoneNumber(phoneNumber);

            calls.ForEach(GenerateRecords);
        }

        private void GenerateRecords(IServiceCall call)
        {
            var charges = GetChargesValidForCountriesInTheCall(call);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private IEnumerable<IServiceCharge> GetChargesValidForCountriesInTheCall(IServiceCall call)
        {
            return _serviceChargeSearch.GetServiceChargesBySubscriptonAndCallType(call.PhoneNumber, call.Type)
                                            .Where(charge => charge.Country == call.FromCountry || charge.Country == call.ToCountry);
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

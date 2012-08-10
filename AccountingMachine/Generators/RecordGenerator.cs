using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core.ServiceCalls;
using Core.ServiceCharges;
using MoreLinq;
using SubscriptionService;

namespace AccountingMachine.Generators
{
    public interface IRecordGenerator
    {
        void GenerateAccountingRecordsForPhoneNumber(string phoneNumber);
    }

    public class RecordGenerator : IRecordGenerator
    {
        private readonly ICallCentral _callCentral;
        private readonly IRecordRepository _recordRepository;
        private readonly ISubscriptionService _subscriptionService;

        public RecordGenerator(ICallCentral callCentral, ISubscriptionService subscriptionService, IRecordRepository recordRepository)
        {
            _callCentral = callCentral;
            _recordRepository = recordRepository;
            _subscriptionService = subscriptionService;
        }


        public void GenerateAccountingRecordsForPhoneNumber(string phoneNumber)
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(phoneNumber);

            calls.ForEach(GenerateRecords);
        }

        private void GenerateRecords(IServiceCall call)
        {
            var charges = GetChargesValidForCountriesInTheCall(call);

            charges.ForEach(charge => GenerateRecord(charge, call));
        }

        private IEnumerable<IServiceCharge> GetChargesValidForCountriesInTheCall(IServiceCall call)
        {
            return _subscriptionService.GetServiceChargesSupportedBy(call.PhoneNumber, call.Type)
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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using Core.Models;
using Core.Repositories;
using MoreLinq;

namespace AccountingMachine.Generators
{
    public interface IBillGenerator
    {
        Bill GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class BillGenerator : IBillGenerator
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IDiscountRepository _discountRepository;

        public BillGenerator(ISubscriptionRepository subscriptionRepository, IRecordRepository recordRepository, IDiscountRepository discountRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _recordRepository = recordRepository;
            _discountRepository = discountRepository;
        }

        public Bill GenerateBillForPhoneNumber(string phoneNumber)
        {
            var subscription = _subscriptionRepository.GetSubscriptionForPhoneNumber(phoneNumber);

            var records = _recordRepository.GetRecordsForPhoneNumber(phoneNumber);

            var discount = _discountRepository.GetDiscountForCustomerStatus(subscription.Customer.Status);

            return new Bill(subscription, records, discount);
        }
    }
}

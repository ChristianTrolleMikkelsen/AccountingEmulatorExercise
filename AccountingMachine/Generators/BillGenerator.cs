using AccountingMachine.Models;
using AccountingMachine.Repositories;
using SubscriptionServices;

namespace AccountingMachine.Generators
{
    public interface IBillGenerator
    {
        Bill GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class BillGenerator : IBillGenerator
    {
        private readonly ISubscriptionSearch _subscriptionSearch;
        private readonly IRecordRepository _recordRepository;
        private readonly IDiscountRepository _discountRepository;

        public BillGenerator(ISubscriptionSearch subscriptionSearch, IRecordRepository recordRepository, IDiscountRepository discountRepository)
        {
            _subscriptionSearch = subscriptionSearch;
            _recordRepository = recordRepository;
            _discountRepository = discountRepository;
        }

        public Bill GenerateBillForPhoneNumber(string phoneNumber)
        {
            var subscription = _subscriptionSearch.GetSubscription(phoneNumber);

            var records = _recordRepository.GetRecordsForPhoneNumber(phoneNumber);

            var discount = _discountRepository.GetDiscountForCustomerStatus(subscription.Customer.Status);

            return new Bill(subscription, records, discount);
        }
    }
}

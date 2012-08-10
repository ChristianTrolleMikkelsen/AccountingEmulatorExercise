using AccountingMachine.Models;
using AccountingMachine.Repositories;
using SubscriptionService;

namespace AccountingMachine.Generators
{
    public interface IBillGenerator
    {
        Bill GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class BillGenerator : IBillGenerator
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRecordRepository _recordRepository;
        private readonly IDiscountRepository _discountRepository;

        public BillGenerator(ISubscriptionService subscriptionService, IRecordRepository recordRepository, IDiscountRepository discountRepository)
        {
            _subscriptionService = subscriptionService;
            _recordRepository = recordRepository;
            _discountRepository = discountRepository;
        }

        public Bill GenerateBillForPhoneNumber(string phoneNumber)
        {
            var subscription = _subscriptionService.GetSubscription(phoneNumber);

            var records = _recordRepository.GetRecordsForPhoneNumber(phoneNumber);

            var discount = _discountRepository.GetDiscountForCustomerStatus(subscription.Customer.Status);

            return new Bill(subscription, records, discount);
        }
    }
}

using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core.Models;
using StructureMap;
using SubscriptionService;
using SubscriptionService.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest
{
    internal abstract class AcceptanceTestFixtureBase
    {
        protected ICallCentral _callCentral;
        protected IAccountingMachine _accountingMachine;
        protected IRecordRepository _recordRepository;
        protected IDiscountRepository _discountRepository;
        protected ISubscriptionService _subscriptionService;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AccountingMachine.AppConfigurator().Initialize();

            new CallCentral.AppConfigurator().Initialize();

            new SubscriptionService.AppConfigurator().Initialize();
        }

        [BeforeScenario]
        public void CreateHelpers()
        {
            _callCentral = ObjectFactory.GetInstance<ICallCentral>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _recordRepository = ObjectFactory.GetInstance<IRecordRepository>();
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
            _subscriptionService = ObjectFactory.GetInstance<ISubscriptionService>();
        }

        [BeforeScenario]
        public void CreateCustomerStatusDiscount()
        {
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.Normal, 0.0M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.HighRoller, 0.1M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.VIP, 0.2M));
        }

       /* protected ISubscriptionSubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,string phoneNumber, string country, CustomerStatus status)
        {
            var customer = _subscriptionService.CreateCustomer("John Doe", status);
            _subscriptionService.CreateSubscription(customer, phoneNumber, country);

            var subscription = _subscriptionService.GetSubscription(phoneNumber);

            ScenarioContext.Current.Set(_subscriptionService.GetSubscription(phoneNumber));
            return subscription;
        }*/
    }
}

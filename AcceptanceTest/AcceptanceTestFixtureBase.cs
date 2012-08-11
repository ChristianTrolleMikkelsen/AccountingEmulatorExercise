using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallServices;
using Core.Models;
using StructureMap;
using SubscriptionServices;
using TechTalk.SpecFlow;

namespace AcceptanceTest
{
    internal abstract class AcceptanceTestFixtureBase
    {
        protected ICallRegistration _callRegistration;
        protected ICallSearch _callSearch;
        protected IAccountingMachine _accountingMachine;
        protected IRecordRepository _recordRepository;
        protected IDiscountRepository _discountRepository;
        protected ICustomerRegistration _customerRegistration;
        protected ISubscriptionRegistration _subscriptionRegistration;
        protected IServiceRegistration _serviceRegistration;
        protected IServiceChargeRegistration _serviceChargeRegistration;
        protected IServiceSearch _serviceSearch;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AccountingMachine.AppConfigurator().Initialize();

            new CallServices.AppConfigurator().Initialize();

            new SubscriptionServices.AppConfigurator().Initialize();
        }

        [BeforeScenario]
        public void CreateHelpers()
        {
            _callRegistration = ObjectFactory.GetInstance<ICallRegistration>();
            _callSearch = ObjectFactory.GetInstance<ICallSearch>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _recordRepository = ObjectFactory.GetInstance<IRecordRepository>();
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
            _customerRegistration = ObjectFactory.GetInstance<ICustomerRegistration>();
            _subscriptionRegistration = ObjectFactory.GetInstance<ISubscriptionRegistration>();
            _serviceRegistration = ObjectFactory.GetInstance<IServiceRegistration>();
            _serviceChargeRegistration = ObjectFactory.GetInstance<IServiceChargeRegistration>();
            _serviceSearch = ObjectFactory.GetInstance<IServiceSearch>();
        }

        [BeforeScenario]
        public void CreateCustomerStatusDiscount()
        {
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.Normal, 0.0M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.HighRoller, 0.1M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.VIP, 0.2M));
        }
    }
}

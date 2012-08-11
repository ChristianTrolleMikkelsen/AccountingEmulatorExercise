using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallServices;
using ChargeServices;
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
        protected IServiceChargeRegistration _serviceChargeRegistration;
        protected IServiceChargeSearch _serviceChargeSearch;

        protected static readonly decimal SecondUnitSize = 1;
        protected static readonly decimal MinuteUnitSize = 60;
        protected static readonly decimal OneKiloByteUnitSize = 1024M;
        protected static readonly decimal OneMegaByteUnitSize = 1024M * 1024M;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AccountingMachine.AppConfigurator().Initialize();

            new CallServices.AppConfigurator().Initialize();

            new SubscriptionServices.AppConfigurator().Initialize();

            new ChargeServices.AppConfigurator().Initialize();
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
            _serviceChargeRegistration = ObjectFactory.GetInstance<IServiceChargeRegistration>();
            _serviceChargeSearch = ObjectFactory.GetInstance<IServiceChargeSearch>();
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

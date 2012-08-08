using NUnit.Framework;
using PhoneSubscriptionCalculator;
using PhoneSubscriptionCalculator.Factories;
using PhoneSubscriptionCalculator.Repositories;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest
{
    [TestFixture]
    internal abstract class AcceptanceTestFixtureBase
    {
        protected IPhoneSubscriptionFactory _subscriptionFactory;
        protected ISubscriptionRepository _subscriptionRepository;
        protected IServiceRepository _serviceRepository;
        protected ICallCentral _callCentral;
        protected IServiceChargeRepository _serviceChargeRepository;
        protected IAccountingMachine _accountingMachine;
        protected IRecordRepository _recordRepository;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AppConfigurator().Initialize();
        }

        [BeforeScenario]
        public void CreateHelpers()
        {
            _subscriptionFactory = ObjectFactory.GetInstance<IPhoneSubscriptionFactory>();
            _subscriptionRepository = ObjectFactory.GetInstance<ISubscriptionRepository>();
            _serviceRepository = ObjectFactory.GetInstance<IServiceRepository>();
            _callCentral = ObjectFactory.GetInstance<ICallCentral>();
            _serviceChargeRepository = ObjectFactory.GetInstance<IServiceChargeRepository>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _recordRepository = ObjectFactory.GetInstance<IRecordRepository>();
        }
    }
}

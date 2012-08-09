using AccountingMachine;
using AccountingMachine.Repositories;
using CallCentral;
using Core.Repositories;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest
{
    internal abstract class AcceptanceTestFixtureBase
    {
        protected ISubscriptionRepository _subscriptionRepository;
        protected IServiceRepository _serviceRepository;
        protected ICallCentral _callCentral;
        protected IServiceChargeRepository _serviceChargeRepository;
        protected IAccountingMachine _accountingMachine;
        protected IRecordRepository _recordRepository;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AccountingMachine.AppConfigurator().Initialize();

            new CallCentral.AppConfigurator().Initialize();
        }

        [BeforeScenario]
        public void CreateHelpers()
        {
            _subscriptionRepository = ObjectFactory.GetInstance<ISubscriptionRepository>();
            _serviceRepository = ObjectFactory.GetInstance<IServiceRepository>();
            _callCentral = ObjectFactory.GetInstance<ICallCentral>();
            _serviceChargeRepository = ObjectFactory.GetInstance<IServiceChargeRepository>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _recordRepository = ObjectFactory.GetInstance<IRecordRepository>();
        }
    }
}

﻿using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core.Models;
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
        protected IDiscountRepository _discountRepository;

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
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
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

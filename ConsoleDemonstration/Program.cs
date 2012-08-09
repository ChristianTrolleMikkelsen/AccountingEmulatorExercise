using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core.Models;
using Core.Repositories;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using StructureMap;

namespace ConsoleDemonstration
{
    class Program
    {
        private static ISubscriptionRepository _subscriptionRepository;
        private static IServiceRepository _serviceRepository;
        private static ICallCentral _callCentral;
        private static IServiceChargeRepository _serviceChargeRepository;
        private static IAccountingMachine _accountingMachine;
        private static Subscription _subscription;
        private static IDiscountRepository _discountRepository;

        static void Main(string[] args)
        {
            Initialize();

            SetupData();

            var customer = new Customer("John CallALot");
            customer.SetCustomerStatus(CustomerStatus.HighRoller);

            _subscription = new Subscription(customer, "90909090", "DK");
            _subscriptionRepository.SaveSubscription(_subscription);

            CreateOneOfEachServiceWithOneOrMoreCharges();

            PerformCalls();

            var bill = _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);

            Console.WriteLine(bill.ToString());
        }

        private static void Initialize()
        {
            new AccountingMachine.AppConfigurator().Initialize();
            new CallCentral.AppConfigurator().Initialize();

            _subscriptionRepository = ObjectFactory.GetInstance<ISubscriptionRepository>();
            _serviceRepository = ObjectFactory.GetInstance<IServiceRepository>();
            _callCentral = ObjectFactory.GetInstance<ICallCentral>();
            _serviceChargeRepository = ObjectFactory.GetInstance<IServiceChargeRepository>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
        }

        private static void SetupData()
        {
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.Normal, 0.0M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.HighRoller, 0.1M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.VIP, 0.2M));
        }

        private static void CreateOneOfEachServiceWithOneOrMoreCharges()
        {
            _serviceRepository.SaveService(new VoiceService(_subscription.PhoneNumber));
            _serviceRepository.SaveService(new SMSService(_subscription.PhoneNumber));
            _serviceRepository.SaveService(new DataTransferService(_subscription.PhoneNumber));

            //Local
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, typeof(VoiceService), 0.20M, "Standard Call Charge", "DK"));
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(VoiceService), 0.50M, 60, "Standard Minute Charge", "DK"));

            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, typeof(SMSService), 0.10M, "Standard Send SMS Charge", "DK"));
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(SMSService), 0.50M, 10, "Standard SMS Lenght Charge", "DK"));

            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(DataTransferService), 0.50M, 1024*1024, "Standard Megabyte Charge", "DK"));

            //Germany
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, typeof(VoiceService), 0.40M, "Standard Call Charge", "DE"));
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(VoiceService), 1.50M, 60, "Standard Minute Charge", "DE"));

            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, typeof(SMSService), 0.20M, "Standard Send SMS Charge", "DE"));
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(SMSService), 2.50M, 10, "Standard SMS Lenght Charge", "DE"));

            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber, typeof(DataTransferService), 7.50M, 1024 * 1024, "Standard Megabyte Charge", "DE"));
        }

        private static void PerformCalls()
        {
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-1), new TimeSpan(0, 0, 3, 35), "11111111", "DK", "DK"));
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), new TimeSpan(0, 0, 0, 21), "22222222", "DK", "DK"));
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-4), new TimeSpan(0, 0, 5, 45), "33333333", "DK", "DE"));
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-3), new TimeSpan(0, 0, 9, 17), "44444444", "DE", "DK"));
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-6), new TimeSpan(0, 0, 2, 01), "55555555", "DE", "DE"));

            _callCentral.RegisterACall(new SMSServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 100, "66666666", "DK", "DK"));
            _callCentral.RegisterACall(new SMSServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 150, "77777777", "DE", "DK"));
            _callCentral.RegisterACall(new SMSServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-4), 100, "88888888", "DK", "DE"));
            _callCentral.RegisterACall(new SMSServiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-5), 100, "99999999", "DE", "DE"));

            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-5), 12 * 1024 * 1024, "www.google.com", "DK", "DK"));
            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-6), 3 * 1024 * 1024, "www.google.com", "DK", "DE"));
            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 1050 * 1024 * 1024, "www.google.com", "DE", "DK"));
            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-1), 1 * 1024 * 1024, "www.google.com", "DE", "DE"));
        }
    }
}

using System;
using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallCentral;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using StructureMap;
using SubscriptionService;
using SubscriptionService.Services;

namespace ConsoleDemonstration
{
    class Program
    {
        private static ICallCentral _callCentral;
        private static IAccountingMachine _accountingMachine;
        private static ISubscription _subscription;
        private static IDiscountRepository _discountRepository;
        private static ISubscriptionService _subscriptionService;

        static void Main(string[] args)
        {
            Initialize();

            SetupData();

            var customer = _subscriptionService.CreateCustomer("John CallALot", CustomerStatus.HighRoller);
            _subscriptionService.CreateSubscription(customer, "90909090", "DK");
            _subscription = _subscriptionService.GetSubscription("90909090");

            CreateOneOfEachServiceWithOneOrMoreCharges();

            PerformCalls();

            var bill = _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);

            Console.WriteLine(bill.ToString());
        }

        private static void Initialize()
        {
            new AccountingMachine.AppConfigurator().Initialize();
            new CallCentral.AppConfigurator().Initialize();
            new SubscriptionService.AppConfigurator().Initialize();

            _callCentral = ObjectFactory.GetInstance<ICallCentral>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
            _subscriptionService = ObjectFactory.GetInstance<ISubscriptionService>();
        }

        private static void SetupData()
        {
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.Normal, 0.0M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.HighRoller, 0.1M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.VIP, 0.2M));
        }

        private static void CreateOneOfEachServiceWithOneOrMoreCharges()
        {
            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.Voice));
            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.SMS));
            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.DataTransfer));

            //Local
            _subscriptionService.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.20M, "Standard Call Charge", "DK"));
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.50M, 60, "Standard Minute Charge", "DK"));

            _subscriptionService.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.10M, "Standard Send SMS Charge", "DK"));
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.50M, 10, "Standard SMS Lenght Charge", "DK"));

            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.DataTransfer, 0.50M, 1024 * 1024, "Standard Megabyte Charge", "DK"));

            //Germany
            _subscriptionService.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.40M, "Standard Call Charge", "DE"));
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.Voice, 1.50M, 60, "Standard Minute Charge", "DE"));

            _subscriptionService.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.20M, "Standard Send SMS Charge", "DE"));
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.SMS, 2.50M, 10, "Standard SMS Lenght Charge", "DE"));

            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.DataTransfer, 7.50M, 1024 * 1024, "Standard Megabyte Charge", "DE"));
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

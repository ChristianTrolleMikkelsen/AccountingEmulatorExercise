using System;
using AccountingMachine;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using CallServices;
using CallServices.Calls;
using Core;
using Core.Models;
using StructureMap;
using SubscriptionServices;
using SubscriptionServices.ServiceCharges;

namespace ConsoleDemonstration
{
    class Program
    {
        private static ICallRegistration _callRegistration;
        private static IAccountingMachine _accountingMachine;
        private static ISubscription _subscription;
        private static IDiscountRepository _discountRepository;
        private static ICustomerRegistration _customerRegistration;
        private static ISubscriptionRegistration _subscriptionRegistration;
        private static ISubscriptionSearch _subscriptionSearch;
        private static IServiceRegistration _serviceRegistration;
        private static IServiceChargeRegistration _serviceChargeRegistration;

        static void Main(string[] args)
        {
            Initialize();

            SetupData();

            var customer = _customerRegistration.CreateCustomer("John CallALot", CustomerStatus.HighRoller);
            _subscriptionRegistration.CreateSubscription(customer, "90909090", "DK");
            _subscription = _subscriptionSearch.GetSubscription("90909090");

            CreateOneOfEachServiceWithOneOrMoreCharges();

            PerformCalls();

            var bill = _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);

            Console.WriteLine(bill.ToString());
        }

        private static void Initialize()
        {
            new AccountingMachine.AppConfigurator().Initialize();
            new CallServices.AppConfigurator().Initialize();
            new SubscriptionServices.AppConfigurator().Initialize();

            _callRegistration = ObjectFactory.GetInstance<ICallRegistration>();
            _accountingMachine = ObjectFactory.GetInstance<IAccountingMachine>();
            _discountRepository = ObjectFactory.GetInstance<IDiscountRepository>();
            _customerRegistration = ObjectFactory.GetInstance<ICustomerRegistration>();
            _subscriptionRegistration = ObjectFactory.GetInstance<ISubscriptionRegistration>();
            _subscriptionSearch = ObjectFactory.GetInstance<ISubscriptionSearch>();
            _serviceRegistration = ObjectFactory.GetInstance<IServiceRegistration>();
            _serviceChargeRegistration = ObjectFactory.GetInstance<IServiceChargeRegistration>();
        }

        private static void SetupData()
        {
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.Normal, 0.0M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.HighRoller, 0.1M));
            _discountRepository.SaveDiscount(new Discount(CustomerStatus.VIP, 0.2M));
        }

        private static void CreateOneOfEachServiceWithOneOrMoreCharges()
        {
            _serviceRegistration.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.Voice));
            _serviceRegistration.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.SMS));
            _serviceRegistration.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.DataTransfer));

            //Local
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.20M, "Standard Call Charge", "DK"));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.50M, 60, "Standard Minute Charge", "DK"));

            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.10M, "Standard Send SMS Charge", "DK"));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.50M, 10, "Standard SMS Lenght Charge", "DK"));

            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.DataTransfer, 0.50M, 1024 * 1024, "Standard Megabyte Charge", "DK"));

            //Germany
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 0.40M, "Standard Call Charge", "DE"));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.Voice, 1.50M, 60, "Standard Minute Charge", "DE"));

            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 0.20M, "Standard Send SMS Charge", "DE"));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.SMS, 2.50M, 10, "Standard SMS Lenght Charge", "DE"));

            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber, ServiceType.DataTransfer, 7.50M, 1024 * 1024, "Standard Megabyte Charge", "DE"));
        }

        private static void PerformCalls()
        {
            _callRegistration.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-1), new TimeSpan(0, 0, 3, 35), "11111111", "DK", "DK"));
            _callRegistration.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), new TimeSpan(0, 0, 0, 21), "22222222", "DK", "DK"));
            _callRegistration.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-4), new TimeSpan(0, 0, 5, 45), "33333333", "DK", "DE"));
            _callRegistration.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-3), new TimeSpan(0, 0, 9, 17), "44444444", "DE", "DK"));
            _callRegistration.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-6), new TimeSpan(0, 0, 2, 01), "55555555", "DE", "DE"));

            _callRegistration.RegisterACall(new SMSCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 100, "66666666", "DK", "DK"));
            _callRegistration.RegisterACall(new SMSCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 150, "77777777", "DE", "DK"));
            _callRegistration.RegisterACall(new SMSCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-4), 100, "88888888", "DK", "DE"));
            _callRegistration.RegisterACall(new SMSCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-5), 100, "99999999", "DE", "DE"));

            _callRegistration.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-5), 12 * 1024 * 1024, "www.google.com", "DK", "DK"));
            _callRegistration.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-6), 3 * 1024 * 1024, "www.google.com", "DK", "DE"));
            _callRegistration.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-2), 1050 * 1024 * 1024, "www.google.com", "DE", "DK"));
            _callRegistration.RegisterACall(new DataTransferCall(_subscription.PhoneNumber, DateTime.Now.AddDays(-1), 1 * 1024 * 1024, "www.google.com", "DE", "DE"));
        }
    }
}

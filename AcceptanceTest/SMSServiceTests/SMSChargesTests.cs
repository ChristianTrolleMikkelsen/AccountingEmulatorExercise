using System;
using System.Linq;
using CallServices.Calls;
using ChargeServices.ServiceCharges;
using Core;
using Core.Models;
using FluentAssertions;
using SubscriptionServices;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.SMSServiceTests
{
    [Binding]
    class SMSChargesTests : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private int _smsLenght;
        private string _fromCountry;
        private string _toCountry;

        [Given(@"I have added a SMS service to the subscription")]
        public void GivenIHaveAddedASMSServiceToTheSubscription()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,"66665555", "DK", CustomerStatus.Normal);
        }

        [Given(@"I have specified a SMS send charge of ""(.*)""")]
        public void GivenIHaveSpecifiedASMSSendChargeOf1_0(string charge)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, 
                                                                            ServiceType.SMS,
                                                                            Decimal.Parse(charge), 
                                                                            "Standard SMS Send Charge",
                                                                            "DK"));
        }

        [Given(@"I have added SMS send charge of ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedSMSSendChargeOf0ForCallingToOrFromCountryDE(string charge)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber,
                                                                            ServiceType.SMS,
                                                                            Decimal.Parse(charge),
                                                                            "Standard SMS Send Charge",
                                                                            "DE"));
        }

        [Given(@"I create a SMS of ""(.*)"" characters")]
        public void GivenICreateASMSOf128Characters(string smsLenght)
        {
            _smsLenght = int.Parse(smsLenght);
        }

        [Given(@"the SMS is sent from ""(.*)""")]
        public void GivenTheSMSIsSentFromDK(string country)
        {
            _fromCountry = country;
        }

        [Given(@"the SMS is sent to ""(.*)""")]
        public void GivenTheSMSIsSentToDK(string country)
        {
            _toCountry = country;
        }

        [When(@"the SMS has been sent")]
        public void WhenTheSMSHasBeenSent()
        {
            _callRegistration.RegisterACall(new SMSCall(  _subscription.PhoneNumber,
                                                            DateTime.Now,
                                                            _smsLenght,
                                                            "99999999",
                                                            _fromCountry,
                                                            _toCountry));
        }

        [When(@"the accounting machine has processed the sms call")]
        public void WhenTheAccountingMachineHasProcessedTheSmsCall()
        {
            _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);
        }

        [Then(@"the price for sending the sms must be: ""(.*)""")]
        public void ThenThePriceForSendingTheSmsMustBe1(string price)
        {
            var records = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            records.Sum(record => record.Charge).Should().Be(Decimal.Parse(price));
        }

        [Given(@"I have specified a lenght charge of: ""(.*)"" for every ""(.*)"" character for the SMS Call service")]
        public void GivenIHaveSpecifiedALenghtChargeOf1_0ForEvery128CharacterForTheSMSCallService(string charge, string chunkSize)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                                ServiceType.SMS,
                                                                                Decimal.Parse(charge), 
                                                                                int.Parse(chunkSize),
                                                                                "Standard SMS Lenght Charge",
                                                                                "DK"));
        }

        [Given(@"I have added SMS lenght charge of ""(.*)""  for every ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedSMSLenghtChargeOf4_0ForEvery64ForCallingToOrFromCountryDE(string charge, string chunkSize)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                                ServiceType.SMS,
                                                                                Decimal.Parse(charge),
                                                                                int.Parse(chunkSize),
                                                                                "Standard SMS Lenght Charge",
                                                                                "DE"));
        }
    }
}

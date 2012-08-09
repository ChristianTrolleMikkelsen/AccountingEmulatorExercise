using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using TechTalk.SpecFlow;

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
            _subscription = ScenarioContext.Current.Get<ISubscription>();

            _subscriptionRepository.SaveSubscription(_subscription);

            _serviceRepository.SaveService(new SMSService(_subscription.PhoneNumber));
        }

        [Given(@"I have specified a SMS send charge of ""(.*)""")]
        public void GivenIHaveSpecifiedASMSSendChargeOf1_0(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, 
                                                                            typeof(SMSService),
                                                                            Decimal.Parse(charge), 
                                                                            "Standard SMS Send Charge",
                                                                            "DK"));
        }

        [Given(@"I have added SMS send charge of ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedSMSSendChargeOf0ForCallingToOrFromCountryDE(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber,
                                                                            typeof(SMSService),
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
            _callCentral.RegisterACall(new SMSServiceCall(  _subscription.PhoneNumber,
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
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                                typeof(SMSService),
                                                                                Decimal.Parse(charge), 
                                                                                int.Parse(chunkSize),
                                                                                "Standard SMS Lenght Charge",
                                                                                "DK"));
        }

        [Given(@"I have added SMS lenght charge of ""(.*)""  for every ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedSMSLenghtChargeOf4_0ForEvery64ForCallingToOrFromCountryDE(string charge, string chunkSize)
        {
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                                typeof(SMSService),
                                                                                Decimal.Parse(charge),
                                                                                int.Parse(chunkSize),
                                                                                "Standard SMS Lenght Charge",
                                                                                "DE"));
        }
    }
}

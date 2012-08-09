using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using MoreLinq;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.VoiceServiceTests
{
    [Binding]
    class VoiceChargesTests : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "66665555";
        private ISubscription _subscription;
        private string _fromCountry;
        private string _toCountry;
        private TimeSpan _duration;

        [Given(@"I have a subscription which has ""(.*)"" as local country")]
        public void GivenIHaveASubscriptionWhichHasDKAsLocalCountry(string country)
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber, country);
            _subscriptionRepository.SaveSubscription(_subscription);

            ScenarioContext.Current.Set(_subscription);
        }

        [Given(@"I have added a Voice Call service to the subscription")]
        public void GivenIHaveAddedAVoiceCallServiceToTheSubscription()
        {
            _serviceRepository.SaveService(new VoiceService(_subscription.PhoneNumber));
        }

        [Given(@"I have specified a second charge of: ""(.*)"" for the Voice Call service")]
        public void GivenIHaveSpecifiedASecondChargeOf0_5ForTheVoiceCallService(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof (VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               ChargeHelper.SecondUnitSize,
                                                                               "Standard Second Charge",
                                                                               "DK"));
        }

        [Given(@"start a call from ""(.*)""")]
        public void GivenStartACallFromDK(string country)
        {
            _fromCountry = country;
        }

        [Given(@"the call lasts: ""(.*)""")]
        public void GivenTheCallLasts000100(string duration)
        {
            _duration = TimeSpan.Parse(duration);
        }

        [Given(@"the call is made to ""(.*)""")]
        public void GivenTheCallIsMadeToDK(string country)
        {
            _toCountry = country;
        }

        [When(@"the call is completed")]
        public void WhenTheCallIsCompleted()
        {
            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, 
                                                            DateTime.Now, 
                                                            _duration, 
                                                            "99999999", 
                                                            _fromCountry,
                                                            _toCountry));
        }

        [When(@"the accounting machine has processed the call")]
        public void WhenTheAccountingMachineHasProcessedTheCall()
        {
            _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);
        }

        [Then(@"the price must be: ""(.*)""")]
        public void ThenThePriceMustBe30(string price)
        {
            var records = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            records.Sum(record => record.Charge).Should().Be(Decimal.Parse(price));
        }

        [Given(@"I have specified a minute charge of: ""(.*)"" for every minute begun for the Voice Call service")]
        public void GivenIHaveSpecifiedAMinuteChargeOf1_0ForEveryMinuteBegunForTheVoiceCallService(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               ChargeHelper.MinuteUnitSize,
                                                                               "Standard Minute Charge",
                                                                               "DK"));
        }

        [Given(@"I have specified a interval charge of: ""(.*)"" for every ""(.*)"" begun for the Voice Call service")]
        public void GivenIHaveSpecifiedAIntervalChargeOf1_0ForEvery000030BegunForTheVoiceCallService(string charge, string interval)
        {
            var intervalSize = Convert.ToDecimal(TimeSpan.Parse(interval).TotalSeconds);
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               intervalSize,
                                                                               "Standard Minute Charge",
                                                                               "DK"));
        }

        [Given(@"I have specified a call charge of ""(.*)""")]
        public void GivenIHaveSpecifiedACallChargeOf1(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, 
                                                                            typeof(VoiceService),
                                                                            Decimal.Parse(charge),
                                                                            "Standard Call Fee",
                                                                               "DK"));
        }

        [Given(@"I have added call charge of ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedCallChargeOf2_3ForCallingToOrFromCountryDE(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber,
                                                                            typeof(VoiceService),
                                                                            Decimal.Parse(charge),
                                                                            "Standard Call Fee",
                                                                               "DE"));
        }

        [Given(@"I have added second charge of ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedSecondChargeOf3ForCallingToOrFromCountryDE(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               ChargeHelper.SecondUnitSize,
                                                                               "Standard Second Charge",
                                                                               "DE"));
        }

        [Given(@"I have added minute charge of ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedMinuteChargeOf5ForCallingToOrFromCountryDE(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               ChargeHelper.MinuteUnitSize,
                                                                               "Standard Minute Charge",
                                                                               "DE"));
        }

        [Given(@"I have added an interval charge of ""(.*)"" for every ""(.*)"" for calling to or from country: ""DE""")]
        public void GivenIHaveAddedAnIntervalChargeOf5ForEvery000030ForCallingToOrFromCountryDE(string charge, string interval)
        {
            var intervalSize = Convert.ToDecimal(TimeSpan.Parse(interval).TotalSeconds);
            _serviceChargeRepository.SaveServiceCharge(new VariableCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               Decimal.Parse(charge),
                                                                               intervalSize,
                                                                               "Standard Minute Charge",
                                                                               "DE"));
        }
    }
}

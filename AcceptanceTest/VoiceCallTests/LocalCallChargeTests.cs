using System;
using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;
using PhoneSubscriptionCalculator.Services;
using TechTalk.SpecFlow;

namespace AcceptanceTest.VoiceCallTests
{
    [Binding]
    class LocalCallChargeTests : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "66665555";
        private ISubscription _subscription;
        private string _fromCountry;
        private string _toCountry;
        private TimeSpan _duration;

        [Given(@"I have a subscription which has ""(.*)"" as local country")]
        public void GivenIHaveASubscriptionWhichHasDKAsLocalCountry(string country)
        {
            _subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber, country);
            _subscriptionRepository.SaveSubscription(_subscription);
        }

        [Given(@"I have added a Voice Call service to the subscription")]
        public void GivenIHaveAddedAVoiceCallServiceToTheSubscription()
        {
            _serviceRepository.SaveService(new VoiceCallService(_subscription.PhoneNumber));
        }

        [Given(@"I have specified a second charge of: ""(.*)"" for the Voice Call service")]
        public void GivenIHaveSpecifiedASecondChargeOf0_5ForTheVoiceCallService(string charge)
        {
            _serviceChargeRepository.SaveServiceCharge(new VoiceCallSecondCharge(_subscription.PhoneNumber,
                                                                                 Decimal.Parse(charge)));
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
            var bills = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            bills.Count().Should().Be(1);
            bills.First().Charge.Should().Be(Decimal.Parse(price));
        }
    }
}

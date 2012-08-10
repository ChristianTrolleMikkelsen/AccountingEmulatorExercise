using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.ForeignCountryCallsTests
{
    [Binding]
    class ForeignCountryServiceBehaviourTests : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private decimal _dkCharge = 1M;
        private decimal _deCharge = 2M;
        private string _fromCountry;
        private string _toCountry;

        [Given(@"I have a subscription for phone ""23458126"" with local country ""DK""")]
        public void GivenIHaveASubscriptionForPhone23458126WithLocalCountryDK()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer("23458126");

            _subscriptionRepository.SaveSubscription(_subscription);
        }

        [Given(@"the subscription is allowed to perform Voice Calls to ""DE""")]
        public void GivenTheSubscriptionIsAllowedToPerformVoiceCallsToDE()
        {
            _serviceRepository.SaveService(new VoiceService(_subscription.PhoneNumber));
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               _dkCharge,
                                                                               "Standard Second Charge",
                                                                               "DK"));
            _serviceChargeRepository.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber,
                                                                               typeof(VoiceService),
                                                                               _deCharge,
                                                                               "Standard Second Charge",
                                                                               "DE"));
        }

        [Given(@"a Voice Call is performed from ""(.*)"" to ""(.*)""")]
        public void GivenAVoiceCallIsPerformedFromDKToDE(string fromCountry, string toCountry)
        {
            _fromCountry = fromCountry;
            _toCountry = toCountry;

            _callCentral.RegisterACall(new VoiceServiceCall(_subscription.PhoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "77777777", _fromCountry, _toCountry));
        }

        [When(@"the cost of the call is calculated")]
        public void WhenTheCostOfTheCallIsCalculated()
        {
            _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);
        }

        [Then(@"the charge is the sum of both calls")]
        public void ThenTheChargeIsTheSumOfBothCalls()
        {
            var records = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            records.Sum(record => record.Charge).Should().Be(_dkCharge + _deCharge);
        }

        [Then(@"the charge is that of the foreign country only")]
        public void ThenTheChargeIsThatOfTheForeignCountryOnly()
        {
            var records = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            records.Sum(record => record.Charge).Should().Be(_deCharge);
        }
    }
}

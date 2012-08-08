using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator;
using PhoneSubscriptionCalculator.Factories;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Services;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest.CallCentralTests
{
    [Binding]
    class CallCentralTest : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "33334444";

        [Given(@"a customer has a phone subscription with the Voice Call Service")]
        public void GivenACustomerHasAPhoneSubscriptionWithTheVoiceCallService()
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);

            _serviceRepository.SaveService(new VoiceCallService(phoneNumber));

            _subscriptionRepository.SaveSubscription(subscription);
        }

        [When(@"the customer makes a Voice Call with the phone")]
        public void WhenTheCustomerMakesAVoiceCallWithThePhone()
        {
            _callCentral.RegisterACall(new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DK"));
        }


        [Then(@"the call has been registred at the Call Central")]
        public void ThenTheCallHasBeenRegistredAtTheCallCentral()
        {
            _callCentral.GetCallsMadeFromPhoneNumber(phoneNumber).Count().Should().Be(1);
        }

        [Given(@"a customer has a phone subscriptions without any services")]
        public void GivenACustomerHasAPhoneSubscriptionsWithoutAnyServices()
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);

            _subscriptionRepository.SaveSubscription(subscription);
        }

        [When(@"the customer tries to make a Voice Call with the phone")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhone()
        {
            var call = new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DK");
            ScenarioContext.Current.Set(call);
        }

        [Then(@"the service is denied when contacting the Call Central")]
        public void ThenTheServiceIsDeniedWhenContactingTheCallCentral()
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();

            Assert.Throws<Exception>(delegate { _callCentral.RegisterACall(call); });
        }
    }
}

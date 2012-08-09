using System;
using System.Linq;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.CallCentralTests
{
    [Binding]
    class CallCentralTest : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "33334444";

        [Given(@"a customer has a phone subscription with the Voice Call Service")]
        public void GivenACustomerHasAPhoneSubscriptionWithTheVoiceCallService()
        {
            var subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);

            _serviceRepository.SaveService(new VoiceService(phoneNumber));
            _localServiceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(phoneNumber));

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
            var subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);

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

        [When(@"the customer tries to make a Voice Call with the phone to ""DE"" from ""DK""")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhoneToDEFromDK()
        {
            var call = new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DE");
            ScenarioContext.Current.Set(call);
        }
    }
}

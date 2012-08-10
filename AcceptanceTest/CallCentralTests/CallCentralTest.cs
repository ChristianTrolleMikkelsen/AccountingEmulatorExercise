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
        private string _phoneNumber = "33334444";
        private string _destinationNumber = "88889999";
        private string _fromCountry = "DK";
        private string _toCountry = "DK";


        [Given(@"a customer has a phone subscription with the Voice Call Service")]
        public void GivenACustomerHasAPhoneSubscriptionWithTheVoiceCallService()
        {
            var subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_phoneNumber);

            _serviceRepository.SaveService(new VoiceService(_phoneNumber));
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(_phoneNumber));

            _subscriptionRepository.SaveSubscription(subscription);
        }

        [When(@"the customer makes a Voice Call with the phone")]
        public void WhenTheCustomerMakesAVoiceCallWithThePhone()
        {
            _callCentral.RegisterACall(new VoiceServiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry));
        }


        [Then(@"the call has been registred at the Call Central")]
        public void ThenTheCallHasBeenRegistredAtTheCallCentral()
        {
            _callCentral.GetCallsMadeFromPhoneNumber(_phoneNumber).Count().Should().Be(1);
        }

        [Given(@"a customer has a phone subscriptions without any services")]
        public void GivenACustomerHasAPhoneSubscriptionsWithoutAnyServices()
        {
            var subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_phoneNumber);

            _subscriptionRepository.SaveSubscription(subscription);
        }

        [When(@"the customer tries to make a Voice Call with the phone")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhone()
        {
            var call = new VoiceServiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry);
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
            _toCountry = "DE";
            var call = new VoiceServiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry);
            ScenarioContext.Current.Set(call);
        }

        [When(@"for some reason the call is missing a source phonenumber")]
        public void WhenForSomeReasonTheCallIsMissingASourcePhonenumber()
        {
            _phoneNumber = null;
        }

        [When(@"for some reason the call is missing a destination country")]
        public void WhenForSomeReasonTheCallIsMissingADestinationCountry()
        {
            _toCountry = null;
        }

        [When(@"for some reason the call is missing a destination phonenumber")]
        public void WhenForSomeReasonTheCallIsMissingADestinationPhonenumber()
        {
            _destinationNumber = null;
        }

        [When(@"for some reason the call is missing a source country")]
        public void WhenForSomeReasonTheCallIsMissingASourceCountry()
        {
            _fromCountry = null;
        }
    }
}

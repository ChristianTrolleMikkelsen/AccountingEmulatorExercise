using System;
using System.Linq;
using CallCentral.Calls;
using Core;
using Core.Models;
using Core.ServiceCalls;
using FluentAssertions;
using NUnit.Framework;
using SubscriptionService.Services;
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
           SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,_phoneNumber, "DK", CustomerStatus.Normal);

            _subscriptionService.AddServiceToSubscription(new Service(_phoneNumber, ServiceType.Voice));
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_phoneNumber));
        }

        [When(@"the customer makes a Voice Call with the phone")]
        public void WhenTheCustomerMakesAVoiceCallWithThePhone()
        {
            _callCentral.RegisterACall(new VoiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry));
        }


        [Then(@"the call has been registred at the Call Central")]
        public void ThenTheCallHasBeenRegistredAtTheCallCentral()
        {
            _callCentral.GetCallsMadeFromPhoneNumber(_phoneNumber).Count().Should().Be(1);
        }

        [Given(@"a customer has a phone subscriptions without any services")]
        public void GivenACustomerHasAPhoneSubscriptionsWithoutAnyServices()
        {
           SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,_phoneNumber, "DK", CustomerStatus.Normal);
        }

        [When(@"the customer tries to make a Voice Call with the phone")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhone()
        {
            var call = new VoiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry);
            ScenarioContext.Current.Set(call);
        }

        [Then(@"the service is denied when contacting the Call Central")]
        public void ThenTheServiceIsDeniedWhenContactingTheCallCentral()
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();

            Assert.Throws<Exception>(delegate { _callCentral.RegisterACall(call); });
        }

        [When(@"the customer tries to make a Voice Call with the phone to ""DE"" from ""DK""")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhoneToDEFromDK()
        {
            _toCountry = "DE";
            var call = new VoiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry);
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

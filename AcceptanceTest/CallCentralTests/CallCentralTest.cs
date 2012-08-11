using System;
using System.Linq;
using CallServices.Calls;
using ChargeServices.ServiceCharges;
using Core;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using SubscriptionServices;
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
           SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,_phoneNumber, "DK", CustomerStatus.Normal);

            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_phoneNumber, ServiceType.Voice, 1, "Standard Fee", "DK"));
        }

        [When(@"the customer makes a Voice Call with the phone")]
        public void WhenTheCustomerMakesAVoiceCallWithThePhone()
        {
            _callRegistration.RegisterACall(new VoiceCall(_phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, _destinationNumber, _fromCountry, _toCountry));
        }


        [Then(@"the call has been registred at the Call Central")]
        public void ThenTheCallHasBeenRegistredAtTheCallCentral()
        {
            _callSearch.GetCallsMadeFromPhoneNumber(_phoneNumber).Count().Should().Be(1);
        }

        [Given(@"a customer has a phone subscriptions without any services")]
        public void GivenACustomerHasAPhoneSubscriptionsWithoutAnyServices()
        {
           SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,_phoneNumber, "DK", CustomerStatus.Normal);
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

            Assert.Throws<Exception>(delegate { _callRegistration.RegisterACall(call); });
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

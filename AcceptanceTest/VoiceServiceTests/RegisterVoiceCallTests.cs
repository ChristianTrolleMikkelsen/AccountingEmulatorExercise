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

namespace AcceptanceTest.VoiceServiceTests
{
    [Binding]
    class RegisterVoiceCallTests : AcceptanceTestFixtureBase
    {
        private string _phoneNumber;
        private DateTime _startTime;
        private TimeSpan _duration;
        private string _receiver;
        private string _sourceCountry;
        private string _destinationCountry;
        private ISubscription _subscription;

        [Given(@"a subscription with phone number ""(.*)"" exists")]
        public void GivenASubscriptionWithPhoneNumber77889955Exists(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,phoneNumber, "DK", CustomerStatus.Normal);
        }

        [Given(@"the subscription includes the Voice Call Service")]
        public void GivenTheSubscriptionIncludesTheVoiceCallService()
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1, "Standard Fixed Fee", "DK"));
        }

        [Given(@"the customer makes a Voice Call at ""(.*)""")]
        public void GivenTheCustomerMakesAVoiceCallAt090000(string startTime)
        {
            _startTime = DateTime.Parse(startTime);
        }

        [Given(@"the call lasts ""(.*)""")]
        public void GivenTheCallLasts0137(string duration)
        {
            _duration = TimeSpan.Parse(duration);
        }

        [Given(@"the call is made to number: ""(.*)""")]
        public void GivenTheCallIsMadeToNumber98561234(string receiver)
        {
            _receiver = receiver;
        }

        [Given(@"the call is made from: ""(.*)""")]
        public void GivenTheCallIsMadeFromDK(string sourceCountry)
        {
            _sourceCountry = sourceCountry;
        }

        [Given(@"the call is made to: ""(.*)""")]
        public void GivenTheCallIsMadeToDE(string destinationCountry)
        {
            _destinationCountry = destinationCountry;
        }

        [When(@"the call ends")]
        public void WhenTheCallEnds()
        {
            _callRegistration.RegisterACall(new VoiceCall(_phoneNumber, _startTime, _duration, _receiver, _sourceCountry, _destinationCountry));
        }

        [Then(@"I must be able to find the call using the subscription")]
        public void ThenIMustBeAbleToFindTheCallUsingTheSubscription()
        {
            var calls = _callSearch.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber);

            calls.Count().Should().Be(1);

            ScenarioContext.Current.Set(calls.First() as VoiceCall);
        }

        [Then(@"the start time of the call must be registered at ""(.*)""")]
        public void ThenTheStartTimeOfTheCallMustBeRegisteredAt090000(string expectedStartTime)
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();
            call.Start.Should().Be(DateTime.Parse(expectedStartTime));
        }

        [Then(@"the duration of the call must be registered to have lasted ""(.*)"" m:s")]
        public void ThenTheDurationOfTheCallMustBeRegisteredToHaveLasted000137MS(string expectedDuration)
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();
            call.Duration.Should().Be(TimeSpan.Parse(expectedDuration));
        }

        [Then(@"the receiver of the call must be registered as ""(.*)""")]
        public void ThenTheReceiverOfTheCallMustBeRegisteredAs98561234(string expectedPhoneNumber)
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();
            call.DestinationPhoneNumber.Should().Be(expectedPhoneNumber);
        }

        [Then(@"the country from which the call was made must be registered as ""(.*)""")]
        public void ThenTheCountryFromWhichTheCallWasMadeMustBeRegisteredAsDK(string expectedCountry)
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();
            call.FromCountry.Should().Be(expectedCountry);
        }

        [Then(@"the country for which the call was made to must be registered as ""(.*)""")]
        public void ThenTheCountryForWhichTheCallWasMadeToMustBeRegisteredAsDE(string expectedCountry)
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();
            call.ToCountry.Should().Be(expectedCountry);
        }

        [Given(@"the subscription includes support for calling from country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForCallingFromCountryDE(string country)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1, "Standard Fixed Fee", country));
        }

        [Given(@"the subscription includes support for calling to country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForCallingToCountryDK(string country)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1, "Standard Fixed Fee", country));
        }
    }
}

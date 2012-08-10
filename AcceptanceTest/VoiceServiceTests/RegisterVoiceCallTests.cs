﻿using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
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

        [Given(@"a subscription with phone number ""(.*)"" exists")]
        public void GivenASubscriptionWithPhoneNumber77889955Exists(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            var subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);

            _subscriptionRepository.SaveSubscription(subscription);

            ScenarioContext.Current.Set(subscription);
        }

        [Given(@"the subscription includes the Voice Call Service")]
        public void GivenTheSubscriptionIncludesTheVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();

            _serviceRepository.SaveService(new VoiceService(subscription.PhoneNumber));
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(subscription.PhoneNumber));
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
            _callCentral.RegisterACall(new VoiceServiceCall(_phoneNumber, _startTime, _duration, _receiver, _sourceCountry, _destinationCountry));
        }

        [Then(@"I must be able to find the call using the subscription")]
        public void ThenIMustBeAbleToFindTheCallUsingTheSubscription()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();

            var calls = _callCentral.GetCallsMadeFromPhoneNumber(subscription.PhoneNumber);

            calls.Count().Should().Be(1);

            ScenarioContext.Current.Set(calls.First() as VoiceServiceCall);
        }

        [Then(@"the start time of the call must be registered at ""(.*)""")]
        public void ThenTheStartTimeOfTheCallMustBeRegisteredAt090000(string expectedStartTime)
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();
            call.Start.Should().Be(DateTime.Parse(expectedStartTime));
        }

        [Then(@"the duration of the call must be registered to have lasted ""(.*)"" m:s")]
        public void ThenTheDurationOfTheCallMustBeRegisteredToHaveLasted000137MS(string expectedDuration)
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();
            call.Duration.Should().Be(TimeSpan.Parse(expectedDuration));
        }

        [Then(@"the receiver of the call must be registered as ""(.*)""")]
        public void ThenTheReceiverOfTheCallMustBeRegisteredAs98561234(string expectedPhoneNumber)
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();
            call.DestinationPhoneNumber.Should().Be(expectedPhoneNumber);
        }

        [Then(@"the country from which the call was made must be registered as ""(.*)""")]
        public void ThenTheCountryFromWhichTheCallWasMadeMustBeRegisteredAsDK(string expectedCountry)
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();
            call.FromCountry.Should().Be(expectedCountry);
        }

        [Then(@"the country for which the call was made to must be registered as ""(.*)""")]
        public void ThenTheCountryForWhichTheCallWasMadeToMustBeRegisteredAsDE(string expectedCountry)
        {
            var call = ScenarioContext.Current.Get<VoiceServiceCall>();
            call.ToCountry.Should().Be(expectedCountry);
        }

        [Given(@"the subscription includes support for calling from country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForCallingFromCountryDE(string country)
        {
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(_phoneNumber, country));
        }

        [Given(@"the subscription includes support for calling to country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForCallingToCountryDK(string country)
        {
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(_phoneNumber, country));
        }
    }
}

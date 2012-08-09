using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.SubscriptionTests
{
    [Binding]
    class SubscriptionTest : AcceptanceTestFixtureBase
    {
        private string _phoneNumber;
        private string _countryIsoCode;
        private ISubscription _subscription;

        [Given(@"I want to create a new subscription for phone ""(.*)""")]
        public void GivenIWantToCreateANewSubscriptionForPhone51948896(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        [When(@"I have created the subscription")]
        public void WhenIHaveCreatedTheSubscription()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_phoneNumber);
        }

        [Then(@"the subscription is registered for phone ""51948896""")]
        public void ThenTheSubscriptionIsRegisteredForPhone51948896()
        {
            _subscription.PhoneNumber.Should().Be(_subscription.PhoneNumber);
        }

        [Then(@"the subscription has default local country ""DK""")]
        public void ThenTheSubscriptionHasDefaultLocalCountryDK()
        {
            _subscription.LocalCountry.Should().Be("DK");
        }

        [Then(@"the subscription contains an empty list of services")]
        public void ThenTheSubscriptionContainsAnEmptyListOfServices()
        {
            _serviceRepository.GetServicesForPhoneNumber(_subscription.PhoneNumber)
                                .Count().Should().Be(0);
        }

        [Given(@"I have created a subscription for phone ""(.*)""")]
        public void GivenIHaveCreatedASubscriptionForPhone51948896(string phoneNumber)
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);
            _subscriptionRepository.SaveSubscription(_subscription);
        }

        [When(@"I add a new Voice Call service to the subcription")]
        public void WhenIAddANewVoiceCallServiceToTheSubcription()
        {
            _serviceRepository.SaveService(new VoiceService(_subscription.PhoneNumber));
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber));
        }

        [Then(@"the Voice Call service must be added to the list of services")]
        public void ThenTheVoiceCallServiceMustBeAddedToTheListOfServices()
        {
            _serviceRepository.GetServicesForPhoneNumber(_subscription.PhoneNumber)
                                .Count().Should().Be(1);
        }

        [Given(@"I want the local country of the subscription to be ""USD""")]
        public void GivenIWantTheLocalCountryOfTheSubscriptionToBeUSD()
        {
            _countryIsoCode = "USD";
        }

        [When(@"I have created the subscription with a not default country")]
        public void WhenIHaveCreatedTheSubscriptionWithANotDefaultCountry()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_phoneNumber, _countryIsoCode);
        }

        [Then(@"the subscription has local country ""USD""")]
        public void ThenTheSubscriptionHasLocalCountryUSD()
        {
            _subscription.LocalCountry.Should().Be(_subscription.LocalCountry);
        }

        [Given(@"the subscription includes a Voice Call service")]
        public void GivenTheSubscriptionIncludesAVoiceCallService()
        {
            _serviceRepository.SaveService(new VoiceService(_subscription.PhoneNumber));
            _serviceChargeRepository.SaveServiceCharge(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber));
        }

        [When(@"I make a Voice Call with the phone ""51948896""")]
        public void WhenIMakeAVoiceCallWithThePhone51948896()
        {
            var voiceCall = new VoiceServiceCall(_subscription.PhoneNumber,
                                          new DateTime(2012, 1, 1),
                                          new TimeSpan(0, 3, 45),
                                          "33334444",
                                          _subscription.LocalCountry,
                                          _subscription.LocalCountry);
            ScenarioContext.Current.Set(voiceCall);

            _callCentral.RegisterACall(voiceCall);
        }

        [Then(@"I can find the call using the subscription phone number")]
        public void ThenICanFindTheCallUsingTheSubscriptionPhoneNumber()
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber)
                                        .ToList().ConvertAll(call => call as VoiceServiceCall);

            var voiceCall = ScenarioContext.Current.Get<VoiceServiceCall>();

            calls.Count().Should().Be(1);
            calls.First()
                    .ShouldHave().AllProperties().EqualTo(voiceCall);
        }
    }
}

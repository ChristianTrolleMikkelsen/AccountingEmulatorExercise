using System;
using System.Linq;
using CallServices.Calls;
using Core;
using Core.Models;
using Core.ServiceCalls;
using FluentAssertions;
using SubscriptionServices;
using SubscriptionServices.ServiceCharges;
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
        public void GivenIWantToCreateANewSubscriptionForPhone23458126(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        [When(@"I have created the subscription")]
        public void WhenIHaveCreatedTheSubscription()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration, _phoneNumber, "DK", CustomerStatus.Normal);
        }

        [Then(@"the subscription is registered for phone ""23458126""")]
        public void ThenTheSubscriptionIsRegisteredForPhone23458126()
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
            _serviceSearch.GetServicesBySubscription(_subscription.PhoneNumber).Count().Should().Be(0);
        }

        [Given(@"I have created a subscription for phone ""(.*)""")]
        public void GivenIHaveCreatedASubscriptionForPhone23458126(string phoneNumber)
        {
            _subscription =SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,phoneNumber, "DK", CustomerStatus.Normal);
        }

        [When(@"I add a new Voice Call service to the subcription")]
        public void WhenIAddANewVoiceCallServiceToTheSubcription()
        {
            _serviceRegistration.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.Voice));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1, "Standard Fee", "DK"));
        }

        [Then(@"the Voice Call service must be added to the list of services")]
        public void ThenTheVoiceCallServiceMustBeAddedToTheListOfServices()
        {
            _serviceSearch.GetServicesBySubscription(_subscription.PhoneNumber).Count().Should().Be(1);
        }

        [Given(@"I want the local country of the subscription to be ""USD""")]
        public void GivenIWantTheLocalCountryOfTheSubscriptionToBeUSD()
        {
            _countryIsoCode = "USD";
        }

        [When(@"I have created the subscription with a not default country")]
        public void WhenIHaveCreatedTheSubscriptionWithANotDefaultCountry()
        {
            _subscription =SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,_phoneNumber, _countryIsoCode, CustomerStatus.Normal);
        }

        [Then(@"the subscription has local country ""USD""")]
        public void ThenTheSubscriptionHasLocalCountryUSD()
        {
            _subscription.LocalCountry.Should().Be(_subscription.LocalCountry);
        }

        [Given(@"the subscription includes a Voice Call service")]
        public void GivenTheSubscriptionIncludesAVoiceCallService()
        {
            _serviceRegistration.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.Voice));
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1, "Standard Fee", "DK"));
        }

        [When(@"I make a Voice Call with the phone ""23458126""")]
        public void WhenIMakeAVoiceCallWithThePhone23458126()
        {
            var voiceCall = new VoiceCall(_subscription.PhoneNumber,
                                          new DateTime(2012, 1, 1),
                                          new TimeSpan(0, 3, 45),
                                          "33334444",
                                          _subscription.LocalCountry,
                                          _subscription.LocalCountry);
            ScenarioContext.Current.Set(voiceCall);

            _callRegistration.RegisterACall(voiceCall);
        }

        [Then(@"I can find the call using the subscription phone number")]
        public void ThenICanFindTheCallUsingTheSubscriptionPhoneNumber()
        {
            var calls = _callSearch.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber)
                                        .ToList().ConvertAll(call => call as VoiceCall);

            var voiceCall = ScenarioContext.Current.Get<VoiceCall>();

            calls.Count().Should().Be(1);
            calls.First()
                    .ShouldHave().AllProperties().EqualTo(voiceCall);
        }
    }
}

using System;
using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Services;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest.SubscriptionTests
{
    [Binding]
    class SubscriptionTest : AcceptanceTestFixtureBase
    {
        private string _phoneNumber;
        private string _countryIsoCode;

        [Given(@"I want to create a new subscription for phone ""(.*)""")]
        public void GivenIWantToCreateANewSubscriptionForPhone51948896(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        [When(@"I have created the subscription")]
        public void WhenIHaveCreatedTheSubscription()
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(_phoneNumber);
            ScenarioContext.Current.Set(subscription);
        }

        [Then(@"the subscription is registered for phone ""51948896""")]
        public void ThenTheSubscriptionIsRegisteredForPhone51948896()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.PhoneNumber.Should().Be(subscription.PhoneNumber);
        }

        [Then(@"the subscription has default local country ""DK""")]
        public void ThenTheSubscriptionHasDefaultLocalCountryDK()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.LocalCountry.ISOCode.Should().Be("DK");
        }

        [Then(@"the subscription contains an empty list of services")]
        public void ThenTheSubscriptionContainsAnEmptyListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.GetServices().Count().Should().Be(0);
        }

        [Given(@"I have created a subscription for phone ""(.*)""")]
        public void GivenIHaveCreatedASubscriptionForPhone51948896(string phoneNumber)
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);
            ScenarioContext.Current.Set(subscription);
        }

        [When(@"I add a new Voice Call service to the subcription")]
        public void WhenIAddANewVoiceCallServiceToTheSubcription()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.AddService(new VoiceCallService(subscription.PhoneNumber));
        }

        [Then(@"the Voice Call service must be added to the list of services")]
        public void ThenTheVoiceCallServiceMustBeAddedToTheListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.GetServices().Count().Should().Be(1);
        }
        
        [Given(@"the subscription allready contains a Voice Call service")]
        public void GivenTheSubscriptionAllreadyContainsAVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.AddService(new VoiceCallService(subscription.PhoneNumber));
        }

        [When(@"I add a new Voice Call service to the subscription")]
        public void WhenIAddANewVoiceCallServiceToTheSubscription()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.AddService(new VoiceCallService(subscription.PhoneNumber));
        }

        [Then(@"the Voice Call service is not added to the list of services")]
        public void ThenTheVoiceCallServiceIsNotAddedToTheListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.GetServices().Count().Should().Be(1);
        }

        [Given(@"I want the local country of the subscription to be ""USD""")]
        public void GivenIWantTheLocalCountryOfTheSubscriptionToBeUSD()
        {
            _countryIsoCode = "USD";
        }

        [When(@"I have created the subscription with a not default country")]
        public void WhenIHaveCreatedTheSubscriptionWithANotDefaultCountry()
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(_phoneNumber,_countryIsoCode);
            ScenarioContext.Current.Set(subscription);
        }

        [Then(@"the subscription has local country ""USD""")]
        public void ThenTheSubscriptionHasLocalCountryUSD()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();
            subscription.LocalCountry.ISOCode.Should().Be(subscription.LocalCountry.ISOCode);
        }

        [When(@"I make a Voice Call with the phone ""51948896""")]
        public void WhenIMakeAVoiceCallWithThePhone51948896()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();

            var voiceCall = new VoiceCall(subscription.PhoneNumber,
                                          new DateTime(2012, 1, 1),
                                          new TimeSpan(0, 3, 45),
                                          "33334444",
                                          subscription.LocalCountry.ISOCode,
                                          subscription.LocalCountry.ISOCode);
            ScenarioContext.Current.Set(voiceCall);

            var callRepository = ObjectFactory.GetInstance<ICallRepository>();
            callRepository.RegisterACallForPhone(voiceCall);
        }

        [Then(@"I can find the call using the subscription phone number")]
        public void ThenICanFindTheCallUsingTheSubscriptionPhoneNumber()
        {
            var subscription = ScenarioContext.Current.Get<ISubscription>();

            var callRepository = ObjectFactory.GetInstance<ICallRepository>();
            var calls = callRepository.GetCallsMadeByPhone(subscription.PhoneNumber)
                                        .ToList().ConvertAll(call => call as VoiceCall);

            var voiceCall = ScenarioContext.Current.Get<VoiceCall>();

            calls.Count().Should().Be(1);
            calls.First()
                    .ShouldHave().AllProperties().EqualTo(voiceCall);
        }
    }
}

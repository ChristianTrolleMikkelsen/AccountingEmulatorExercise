using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.SubscriptionTests
{
    [Binding]
    class SubscriptionTest
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
            var subscription = new PhoneSubscription(_phoneNumber);
            ScenarioContext.Current.Set(subscription);
        }

        [Then(@"the subscription is registered for phone ""51948896""")]
        public void ThenTheSubscriptionIsRegisteredForPhone51948896()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.PhoneNumber.Should().Be(_phoneNumber);
        }

        [Then(@"the subscription has default local country ""DKK""")]
        public void ThenTheSubscriptionHasDefaultLocalCountryDKK()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.LocalCountry.ISOCode.Should().Be("DKK");
        }

        [Then(@"the subscription contains an empty list of services")]
        public void ThenTheSubscriptionContainsAnEmptyListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.GetServices().Count().Should().Be(0);
        }

        [Given(@"I have created a subscription for phone ""(.*)""")]
        public void GivenIHaveCreatedASubscriptionForPhone51948896(string phoneNumber)
        {
            var subscription = new PhoneSubscription(phoneNumber);
            ScenarioContext.Current.Set(subscription);
        }

        [When(@"I add a new Voice Call service to the subcription")]
        public void WhenIAddANewVoiceCallServiceToTheSubcription()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.AddService(new VoiceCallService());
        }

        [Then(@"the Voice Call service must be added to the list of services")]
        public void ThenTheVoiceCallServiceMustBeAddedToTheListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.GetServices().Count().Should().Be(1);
        }
        
        [Given(@"the subscription allready contains a Voice Call service")]
        public void GivenTheSubscriptionAllreadyContainsAVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.AddService(new VoiceCallService());
        }

        [When(@"I add a new Voice Call service to the subscription")]
        public void WhenIAddANewVoiceCallServiceToTheSubscription()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.AddService(new VoiceCallService());
        }

        [Then(@"the Voice Call service is not added to the list of services")]
        public void ThenTheVoiceCallServiceIsNotAddedToTheListOfServices()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
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
            var subscription = new PhoneSubscription(_phoneNumber,_countryIsoCode);
            ScenarioContext.Current.Set(subscription);
        }

        [Then(@"the subscription has local country ""USD""")]
        public void ThenTheSubscriptionHasLocalCountryUSD()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.LocalCountry.ISOCode.Should().Be(_countryIsoCode);
        }
    }
}

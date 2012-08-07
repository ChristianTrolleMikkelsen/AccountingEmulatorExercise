using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.SubscriptionTests
{
    [Binding]
    class SubscriptionTest
    {
        [Given(@"I want to create a new subscription")]
        public void GivenIWantToCreateANewSubscription()
        {
        }

        [When(@"I have created the subscription")]
        public void WhenIHaveCreatedTheSubscription()
        {
            var subscription = new PhoneSubscription("99998888");

            ScenarioContext.Current.Set<IPhoneSubscription>(subscription);
        }

        [Then(@"the subscription always consists of an empty list of Inland services")]
        public void ThenTheSubscriptionAlwaysConsistsOfAnEmptyListOfInlandServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetInLandServices().Count().Should().Be(0);
        }

        [Then(@"an empty list of Abroad services")]
        public void ThenAnEmptyListOfAbroadServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetAbroadServices().Count().Should().Be(0);
        }

        [Given(@"I have a subscription")]
        public void GivenIHaveASubscription()
        {
            var subscription = new PhoneSubscription("99998888");
            ScenarioContext.Current.Set<IPhoneSubscription>(subscription);
        }

        [When(@"I add a new Voice Call service to the Inland services")]
        public void WhenIAddANewVoiceCallServiceToTheInlandServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            var newService = new VoiceCallService();
            subscription.AddInLandService(newService);
        }

        [When(@"I add a new Voice Call service to the Abroad services")]
        public void WhenIAddANewVoiceCallServiceToTheAbroadServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            var newService = new VoiceCallService();
            subscription.AddAbroadService(newService);
        }

        [Then(@"the Voice Call service must be added the Inland services")]
        public void ThenTheVoiceCallServiceMustBeAddedTheInlandServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetInLandServices().Count().Should().Be(1);
        }

        [Then(@"the Voice Call service must be added the Abroad services")]
        public void ThenTheVoiceCallServiceMustBeAddedTheAbroadServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetAbroadServices().Count().Should().Be(1);
        }

        [Given(@"the Inland services allready contains a Voice Call service")]
        public void GivenTheInlandServicesAllreadyContainsAVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            var newService = new VoiceCallService();
            subscription.AddInLandService(newService);
        }

        [Given(@"the Abroad services allready contains a Voice Call service")]
        public void GivenTheAbroadServicesAllreadyContainsAVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            var newService = new VoiceCallService();
            subscription.AddAbroadService(newService);
        }

        [Then(@"the Voice Call service is not added the Inland services")]
        public void ThenTheVoiceCallServiceIsNotAddedTheInlandServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetInLandServices().Count().Should().Be(1);
        }

        [Then(@"the Voice Call service is not added the Abroad services")]
        public void ThenTheVoiceCallServiceIsNotAddedTheAbroadServices()
        {
            var subscription = ScenarioContext.Current.Get<IPhoneSubscription>();
            subscription.GetAbroadServices().Count().Should().Be(1);
        }
    }
}

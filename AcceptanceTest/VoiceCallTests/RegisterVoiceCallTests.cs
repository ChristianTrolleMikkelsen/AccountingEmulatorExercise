using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest.Voice_Call_Service
{
    [Binding]
    class VoiceCallTests : AcceptanceTestFixtureBase
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
            var subscription = new PhoneSubscription(_phoneNumber);
            ScenarioContext.Current.Set(subscription);
        }

        [Given(@"the subscription includes the Voice Call Service")]
        public void GivenTheSubscriptionIncludesTheVoiceCallService()
        {
            var subscription = ScenarioContext.Current.Get<PhoneSubscription>();
            subscription.AddService(new VoiceCallService());
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
        public void GivenTheCallIsMadeToNumber27206617(string receiver)
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
            var callRepository = ObjectFactory.GetInstance<IPhoneCallRepository>();
            callRepository.RegisterACallForPhone(new VoiceCall(_phoneNumber, _startTime, _duration, _receiver, _sourceCountry, _destinationCountry));
        }

        [Then(@"I must be able to find the call using the subscription")]
        public void ThenIMustBeAbleToFindTheCallUsingTheSubscription()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the start time of the call must be registered at 09:00:00")]
        public void ThenTheStartTimeOfTheCallMustBeRegisteredAt090000()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the duration of the call must be registered to have lasted 01:37")]
        public void ThenTheDurationOfTheCallMustBeRegisteredToHaveLasted0137()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the receiver of the call must be registered as 27206617")]
        public void ThenTheReceiverOfTheCallMustBeRegisteredAs27206617()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the country from which the call was made must be registered as DK")]
        public void ThenTheCountryFromWhichTheCallWasMadeMustBeRegisteredAsDK()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the country for which the call was made to must be registered as DE")]
        public void ThenTheCountryForWhichTheCallWasMadeToMustBeRegisteredAsDE()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

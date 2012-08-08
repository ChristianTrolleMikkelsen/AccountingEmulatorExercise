using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator;
using PhoneSubscriptionCalculator.Factories;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Services;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest.CallCentralTests
{
    [Binding]
    class CallCentralTest : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "33334444";

        [Given(@"a customer has a phone subscription with the Voice Call Service")]
        public void GivenACustomerHasAPhoneSubscriptionWithTheVoiceCallService()
        {
            var subscription = ObjectFactory.GetInstance<IPhoneSubscriptionFactory>()
                                                .CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);

            subscription.AddService(new VoiceCallService(phoneNumber));

            ObjectFactory.GetInstance<ISubscriptionRepository>()
                            .SaveSubscription(subscription);
        }

        [When(@"the customer makes a Voice Call with the phone")]
        public void WhenTheCustomerMakesAVoiceCallWithThePhone()
        {
            var callCentral = ObjectFactory.GetInstance<ICallCentral>();
            callCentral.RegisterACall(new VoiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DK"));
        }


        [Then(@"the call has been registred at the Call Central")]
        public void ThenTheCallHasBeenRegistredAtTheCallCentral()
        {
            var callCentral = ObjectFactory.GetInstance<ICallCentral>();
            callCentral.GetCallsMadeFromPhoneNumber(phoneNumber).Count().Should().Be(1);
        }

        [Given(@"a customer has a phone subscriptions without any services")]
        public void GivenACustomerHasAPhoneSubscriptionsWithoutAnyServices()
        {
            var subscription = ObjectFactory.GetInstance<IPhoneSubscriptionFactory>()
                                                .CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);

            ObjectFactory.GetInstance<ISubscriptionRepository>()
                            .SaveSubscription(subscription);
        }

        [When(@"the customer tries to make a Voice Call with the phone")]
        public void WhenTheCustomerTriesToMakeAVoiceCallWithThePhone()
        {
            var call = new VoiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DK");
            ScenarioContext.Current.Set(call);
        }

        [Then(@"the service is denied when contacting the Call Central")]
        public void ThenTheServiceIsDeniedWhenContactingTheCallCentral()
        {
            var call = ScenarioContext.Current.Get<VoiceCall>();

            var callCentral = ObjectFactory.GetInstance<ICallCentral>();
            Assert.Throws<Exception>(delegate { callCentral.RegisterACall(call); });
        }
    }
}

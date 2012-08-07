using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AcceptanceTest.Voice_Call_Service
{
    [Binding]
	class VoiceCallServiceFeature
	{
        [Given(@"I have a Voice Call service")]
        public void GivenIHaveAVoiceCallService()
        {
           // ScenarioContext.Current.Pending();
        }

        [When(@"a customer performs a Voice Call")]
        public void WhenACustomerPerformsAVoiceCall()
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I must be able to register the Voice Call by:")]
        public void ThenIMustBeAbleToRegisterTheVoiceCallBy(Table table)
        {
            ScenarioContext.Current.Pending();
        }
	}
}

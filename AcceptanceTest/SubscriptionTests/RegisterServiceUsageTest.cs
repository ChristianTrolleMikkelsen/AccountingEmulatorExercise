using System;
using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.SubscriptionTests
{
    [Binding]
    class RegisterServiceUsageTest
    {
        private string fathersPhoneNumber = "11112222";
        private string daughtersPhoneNumber = "22221111";

        [Given(@"a customer has 2 phone subscriptions")]
        public void GivenACustomerHas2PhoneSubscriptions()
        {
            var customer = new Customer("John Doe");

            var ownSubscription = new PhoneSubscription(fathersPhoneNumber);
            var daughtersSubscription = new PhoneSubscription(daughtersPhoneNumber);

            customer.AddPhoneSubscription(ownSubscription);
            customer.AddPhoneSubscription(daughtersSubscription);

            ScenarioContext.Current.Set(customer);
        }

        [Given(@"the customer makes use of all the services provided by both of the subscriptions")]
        public void GivenTheCustomerMakesUseOfAllTheServicesProvidedByBothOfTheSubscriptions()
        {
            var customer = ScenarioContext.Current.Get<Customer>();

            customer.GetPhoneSubscription(fathersPhoneNumber)
                        .RegisterACall(new VoiceCall(new DateTime(1, 1, 1, 8, 0, 2), new TimeSpan(0, 3, 0), daughtersPhoneNumber, "DK", "DK"));

            customer.GetPhoneSubscription(daughtersPhoneNumber)
                        .RegisterACall(new VoiceCall(new DateTime(1, 1, 1, 12, 3, 0), new TimeSpan(0, 2, 0), fathersPhoneNumber, "DK", "DK"))
                        .RegisterACall(new VoiceCall(new DateTime(1, 1, 1, 14, 3, 0), new TimeSpan(0, 42, 0), fathersPhoneNumber, "DK", "DK"))
                        .RegisterACall(new VoiceCall(new DateTime(1, 1, 1, 17, 3, 0), new TimeSpan(0, 32, 0), fathersPhoneNumber, "DK", "DK"));
        }
    
        [When(@"I want to create a bill for the subscriptions")]
        public void WhenIWantToCreateABillForTheSubscriptions()
        {
            
        }
   
        [Then(@"I can inspect every single use of a given service in a subscription order to calculate a bill")]
        public void ThenICanInspectEverySingleUseOfAGivenServiceInASubscriptionOrderToCalculateABill()
        {
            var customer = ScenarioContext.Current.Get<Customer>();

            customer.GetPhoneSubscription(daughtersPhoneNumber)
                        .GetCalls().Count().Should().Be(3);

            customer.GetPhoneSubscription(fathersPhoneNumber)
                        .GetCalls().Count().Should().Be(1);
        }
    }
}

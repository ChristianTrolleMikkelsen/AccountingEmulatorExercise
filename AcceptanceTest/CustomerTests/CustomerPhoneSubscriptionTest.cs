using System.Linq;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.CustomerTests
{
    [Binding]
    class CustomerPhoneSubscriptionTest : AcceptanceTestFixtureBase
    {
        private string phoneNumber = "99998888";
        private Customer customer;

        [Given(@"I have created a new customer")]
        public void GivenIHaveCreatedANewCustomer()
        {
            customer = new Customer("John Doe");
        }

        [When(@"I sell the customer a phone subscription")]
        public void WhenISellTheCustomerAPhoneSubscription()
        {
            var subscription = _subscriptionFactory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry(phoneNumber);
            customer.AddPhoneSubscription(subscription);
        }

        [Then(@"the phone subscription is added to the customers inventory")]
        public void ThenThePhoneSubscriptionIsAddedToTheCustomersInventory()
        {
            customer.GetPhoneSubscriptions().Count(sub => sub.PhoneNumber == phoneNumber).Should().Be(1);
        }
    }
}

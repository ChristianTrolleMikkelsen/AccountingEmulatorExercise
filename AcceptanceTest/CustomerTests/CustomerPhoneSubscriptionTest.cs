using Core.Models;
using FluentAssertions;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.CustomerTests
{
    [Binding]
    class CustomerPhoneSubscriptionTest : AcceptanceTestFixtureBase
    {
        private string _phoneNumber = "99998888";
        private Customer _customer;
        private ISubscription _subscription;

        [Given(@"I have created a new customer")]
        public void GivenIHaveCreatedANewCustomer()
        {
            _customer = new Customer("John Doe");
        }

        [When(@"I sell the customer a phone subscription")]
        public void WhenISellTheCustomerAPhoneSubscription()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_customer, _phoneNumber);
        }

        [Then(@"the phone subscription is assigned the customer")]
        public void ThenThePhoneSubscriptionIsAssignedTheCustomer()
        {
            _subscription.Customer.Should().Be(_customer);
        }

        [Then(@"the customer has the initial customer status NORMAL")]
        public void ThenTheCustomerHasTheInitialCustomerStatusNORMAL()
        {
            _subscription.Customer.Status.Should().Be(CustomerStatus.Normal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.CustomerTests
{
    [Binding]
    class CustomerPhoneSubscriptionTest
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
            var subscription = new PhoneSubscription(phoneNumber);
            customer.AddPhoneSubscription(subscription);
        }

        [Then(@"the phone subscription is added to the customers inventory")]
        public void ThenThePhoneSubscriptionIsAddedToTheCustomersInventory()
        {
            customer.GetPhoneSubscription(phoneNumber).Should().NotBeNull();
        }
    }
}

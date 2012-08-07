using System;
using PhoneSubscriptionCalculator.Models;
using TechTalk.SpecFlow;

namespace AcceptanceTest.CustomerTests
{
    [Binding]
    class CustomerStatusTest
    {
        [Given(@"I have given a customer the ""(.*)"" status on the customers subscription")]
        public void GivenIHaveGivenACustomerTheStatusStatusOnTheCustomersSubscription(string status)
        {
            var customer = new Customer("John Doe");
            customer.SetCustomerStatus((CustomerStatus)Enum.Parse(typeof(CustomerStatus), status));

            ScenarioContext.Current.Set(customer);
        }

        [When(@"I calculate the cost of the customers bill to (\d+)")]
        public void WhenICalculateTheCostOfTheCustomersBillToCalculatedBill(int bill)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"bill receives a (\d+)% discount")]
        public void ThenBillReceivesADiscountDiscount(int discount)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the final bill is (\d+)")]
        public void ThenTheFinalBillIsBillWithDiscount(int expectedBill)
        {
            ScenarioContext.Current.Pending();
        }
    }
}

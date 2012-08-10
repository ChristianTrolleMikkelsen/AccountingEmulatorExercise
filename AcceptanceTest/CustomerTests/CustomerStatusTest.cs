using System;
using AccountingMachine.Models;
using CallCentral.Calls;
using Core;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using FluentAssertions;
using SubscriptionService.ServiceCharges;
using SubscriptionService.Services;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.CustomerTests
{
    [Binding]
    class CustomerStatusTest : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private Bill _bill;

        [Given(@"I have given a customer the ""(.*)"" status")]
        public void GivenIHaveGivenACustomerTheStatusStatus(string status)
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,"12345678", "DK", (CustomerStatus)Enum.Parse(typeof(CustomerStatus), status));

            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber,ServiceType.Voice));
        }

        [Given(@"I charge (\d+) pr Voice Call")]
        public void GivenICharge100PrVoiceCall(int charge)
        {
            _subscriptionService.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber,ServiceType.Voice, charge, "Standard Call Fee", "DK"));
        }

        [Given(@"the customer has finished a call")]
        public void GivenTheCustomerHasFinishedACall()
        {
            _callCentral.RegisterACall(new VoiceCall(_subscription.PhoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "99999999", "DK", "DK"));
        }

        [When(@"I calculate the cost of the customers bill to (\d+)")]
        public void WhenICalculateTheCostOfTheCustomersBillToCalculatedBill(int bill)
        {
            _bill = _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);
            _bill.GetSumOfRecords().Should().Be(bill);
        }

        [Then(@"bill receives a (\d+)% discount")]
        public void ThenBillReceivesADiscountDiscount(int discount)
        {
            _bill.Discount.Percentage.Should().Be(Convert.ToDecimal(discount) / 100);
        }

        [Then(@"the final bill is (\d+)")]
        public void ThenTheFinalBillIsBillWithDiscount(int expectedBill)
        {
            _bill.GetTotal().Should().Be(expectedBill);
        }
    }
}

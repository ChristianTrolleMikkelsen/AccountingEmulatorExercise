using System;
using System.Linq;
using Core;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using SubscriptionService.Services;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.DataTransferTests
{
    [Binding]
    class DataTransferChargesTests : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private string _url;
        private string _fromCountry;
        private string _toCountry;
        private int _size;

        [Given(@"I have added a Data Transfer service to the subscription")]
        public void GivenIHaveAddedADataTransferServiceToTheSubscription()
        {
            _subscription =SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,"66665555", "DK", CustomerStatus.Normal);

            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.DataTransfer));
        }

        [Given(@"I have specified a charge of: ""(.*)"" for every megabyte for the Data Transfer service")]
        public void GivenIHaveSpecifiedAChargeOf10_0ForEveryMegabyteForTheDataTransferService(string charge)
        {
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                          ServiceType.DataTransfer,
                                                                          Convert.ToDecimal(charge),
                                                                          ChargeHelper.OneMegaByteUnitSize,
                                                                          "Standard Megabyte Charge",
                                                                          "DK"));
        }

        [Given(@"I have added Data Transfer charge of ""(.*)""  for every megabyte for calling to or from country: ""(.*)""")]
        public void GivenIHaveAddedDataTransferChargeOf40ForEveryMegabyteForCallingToOrFromCountryDE(string charge, string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                          ServiceType.DataTransfer,
                                                                          Convert.ToDecimal(charge),
                                                                          ChargeHelper.OneMegaByteUnitSize,
                                                                          "Standard Megabyte Charge",
                                                                          country));
        }

        [Given(@"I start a Data Transfer from ""(.*)""")]
        public void GivenIStartADataTransferFromWww_Google_Com(string url)
        {
            _url = url;
        }

        [Given(@"the Data Transfer starts in ""(.*)""")]
        public void GivenTheDataTransferStartsInDK(string country)
        {
            _fromCountry = country;
        }

        [Given(@"the Data Transfer ends in ""(.*)""")]
        public void GivenTheDataTransferEndsInDK(string country)
        {
            _toCountry = country;
        }

        [Given(@"the Data Transfer sizes up to: ""(.*)"" megabytes")]
        public void GivenTheDataTransferSizesUpTo10Megabytes(string size)
        {
            _size = int.Parse(size) * Convert.ToInt32(ChargeHelper.OneMegaByteUnitSize);
        }

        [When(@"the Data Transfer has been completed")]
        public void WhenTheDataTransferHasBeenCompleted()
        {
            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber,
                                                            DateTime.Now,
                                                            _size,
                                                            _url,
                                                            _fromCountry,
                                                            _toCountry));
        }

        [When(@"the accounting machine has processed the data transfer call")]
        public void WhenTheAccountingMachineHasProcessedTheDataTransferCall()
        {
            _accountingMachine.GenerateBillForPhoneNumber(_subscription.PhoneNumber);
        }

        [Then(@"the price for performing the Data Transfer must be: ""(.*)""")]
        public void ThenThePriceForPerformingTheDataTransferMustBe100(string price)
        {
            var records = _recordRepository.GetRecordsForPhoneNumber(_subscription.PhoneNumber);
            records.Sum(record => record.Charge).Should().Be(Decimal.Parse(price));
        }

        [Given(@"the Data Transfer sizes up to: ""(.*)"" kilobytes")]
        public void GivenTheDataTransferSizesUpTo10Kilobytes(string size)
        {
            _size = int.Parse(size) * Convert.ToInt32(ChargeHelper.OneKiloByteUnitSize);
        }

        [Given(@"I have specified a charge of: ""(.*)"" for every kilobyte for the Data Transfer service")]
        public void GivenIHaveSpecifiedAChargeOf10_0ForEveryKilobyteForTheDataTransferService(string charge)
        {
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                          ServiceType.DataTransfer,
                                                                          Convert.ToDecimal(charge),
                                                                          ChargeHelper.OneKiloByteUnitSize,
                                                                          "Standard Kilobyte Charge",
                                                                          "DK"));
        }

        [Given(@"I have added Data Transfer charge of ""(.*)""  for every kilobyte for calling to or from country: ""(.*)""")]
        public void GivenIHaveAddedDataTransferChargeOf40ForEveryKilobyteForCallingToOrFromCountryDE(string charge, string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(new VariableCharge(_subscription.PhoneNumber,
                                                                          ServiceType.DataTransfer,
                                                                          Convert.ToDecimal(charge),
                                                                          ChargeHelper.OneKiloByteUnitSize,
                                                                          "Standard Kilobyte Charge",
                                                                          country));
        }

    }
}

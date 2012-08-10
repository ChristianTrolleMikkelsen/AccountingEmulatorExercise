using System;
using System.Linq;
using CallCentral.Calls;
using Core;
using Core.Models;
using Core.ServiceCalls;
using Core.ServiceCharges;
using FluentAssertions;
using SubscriptionService.Services;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.DataTransferTests
{
    [Binding]
    class RegisterDataTransferTests : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private DateTime _start;
        private int _size;
        private string _url;
        private string _fromCountry;
        private string _toCountry;

        [Given(@"the subscription includes the Data Transfer Service")]
        public void GivenTheSubscriptionIncludesTheDataTransferService()
        {
            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,"11111111", "DK", CustomerStatus.Normal);

            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.DataTransfer));
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber));
        }

        [Given(@"the customer makes a data transfer at ""(.*)""")]
        public void GivenTheCustomerMakesADataTransferAt090000(string start)
        {
            _start = DateTime.Parse(start);
        }

        [Given(@"the ""(.*)"" kb of data is transfered")]
        public void GivenThe1234KbOfDataIsTransfered(string size)
        {
            _size = int.Parse(size);
        }

        [Given(@"the data transfer is from or to: ""(.*)""")]
        public void GivenTheDataTransferIsFromOrToWww_Google_Com(string url)
        {
            _url = url;
        }

        [Given(@"the data transfer is made from: ""(.*)""")]
        public void GivenTheDataTransferIsMadeFromDK(string country)
        {
            _fromCountry = country;
        }

        [Given(@"the data transfer is made to: ""(.*)""")]
        public void GivenTheDataTransferIsMadeToDK(string country)
        {
            _toCountry = country;
        }

        [When(@"the data transfer ends")]
        public void WhenTheDataTransferEnds()
        {
            _callCentral.RegisterACall(new DataTransferCall(_subscription.PhoneNumber,
                                                            _start,
                                                            _size,
                                                            _url,
                                                            _fromCountry,
                                                            _toCountry));
        }

        [Then(@"I must be able to find the data transfer using the subscription")]
        public void ThenIMustBeAbleToFindTheDataTransferUsingTheSubscription()
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber);

            calls.Count().Should().Be(1);

            ScenarioContext.Current.Set(calls.First() as DataTransferCall); 
        }

        [Then(@"the start time of the data transfer must be registered at ""(.*)""")]
        public void ThenTheStartTimeOfTheDataTransferMustBeRegisteredAt090000(string start)
        {
            var sms = ScenarioContext.Current.Get<DataTransferCall>();
            sms.TransferStart.Should().Be(DateTime.Parse(start));
        }

        [Then(@"the size of the data transfer must be registered to be: ""(.*)"" kb")]
        public void ThenTheSizeOfTheDataTransferMustBeRegisteredToBe1234Kb(string size)
        {
            var sms = ScenarioContext.Current.Get<DataTransferCall>();
            sms.Size.Should().Be(int.Parse(size));
        }

        [Then(@"the source of the data transfer must be registered as ""(.*)""")]
        public void ThenTheSourceOfTheDataTransferMustBeRegisteredAsWww_Google_Com(string url)
        {
            var sms = ScenarioContext.Current.Get<DataTransferCall>();
            sms.TransferUrl.Should().Be(url);
        }

        [Then(@"the country from which the data transfer was made must be registered as ""(.*)""")]
        public void ThenTheCountryFromWhichTheDataTransferWasMadeMustBeRegisteredAsDK(string country)
        {
            var sms = ScenarioContext.Current.Get<DataTransferCall>();
            sms.FromCountry.Should().Be(country);
        }

        [Then(@"the country for which the data transfer was made to must be registered as ""(.*)""")]
        public void ThenTheCountryForWhichTheDataTransferWasMadeToMustBeRegisteredAsDK(string country)
        {
            var sms = ScenarioContext.Current.Get<DataTransferCall>();
            sms.ToCountry.Should().Be(country);
        }

        [Given(@"the subscription includes support for transfering data from country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTransferingDataFromCountryDK(string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber, country));
        }

        [Given(@"the subscription includes support for transfering data to country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTransferingDataToCountryDE(string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber, country));
        }
    }
}

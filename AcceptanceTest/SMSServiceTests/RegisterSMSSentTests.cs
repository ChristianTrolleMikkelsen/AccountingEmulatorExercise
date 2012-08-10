using System;
using System.Linq;
using Core.Models;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
using SubscriptionService.Services;
using TechTalk.SpecFlow;
using TestHelpers;

namespace AcceptanceTest.SMSServiceTests
{
    [Binding]
    class RegisterSMSSentTests : AcceptanceTestFixtureBase
    {
        private ISubscription _subscription;
        private int _smsLenght;
        private string _receiver;
        private string _fromCountry;
        private string _toCountry;

        [Given(@"the subscription includes the SMS Service")]
        public void GivenTheSubscriptionIncludesTheSMSService()
        {
            _subscription =SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionService,"66665555", "DK", CustomerStatus.Normal);

            _subscriptionService.AddServiceToSubscription(new Service(_subscription.PhoneNumber, ServiceType.SMS));

            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber));
        }

        [Given(@"the customer creates an SMS")]
        public void GivenTheCustomerCreatesAnSMS()
        {
        }

        [Given(@"the SMS has the lenght ""(.*)"" characters")]
        public void GivenTheSMSHasTheLenght128Characters(string lenght)
        {
            _smsLenght = int.Parse(lenght);
        }

        [Given(@"the SMS is sent to number: ""(.*)""")]
        public void GivenTheSMSIsSentToNumber98561234(string phoneNumber)
        {
            _receiver = phoneNumber;
        }

        [Given(@"the SMS is sent from: ""(.*)""")]
        public void GivenTheSMSIsSentFromDK(string country)
        {
            _fromCountry = country;
        }

        [Given(@"the SMS is sent to: ""(.*)""")]
        public void GivenTheSMSIsSentToDE(string country)
        {
            _toCountry = country;
        }

        [When(@"the SMS is sent at ""(.*)""")]
        public void WhenTheSMSIsSentAt090000(string startTime)
        {
            _callCentral.RegisterACall(new SMSServiceCall(  _subscription.PhoneNumber,
                                                            DateTime.Parse(startTime),
                                                            _smsLenght,
                                                            _receiver,
                                                            _fromCountry,
                                                            _toCountry));
        }

        [Then(@"I must be able to find the SMS using the subscription")]
        public void ThenIMustBeAbleToFindTheSMSUsingTheSubscription()
        {
            var calls = _callCentral.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber);

            calls.Count().Should().Be(1);

            ScenarioContext.Current.Set(calls.First() as SMSServiceCall); 
        }

        [Then(@"the start time of the SMS must be registered at ""(.*)""")]
        public void ThenTheStartTimeOfTheSMSMustBeRegisteredAt090000(string expectedStart)
        {
            var sms = ScenarioContext.Current.Get<SMSServiceCall>();
            sms.SendTime.Should().Be(DateTime.Parse(expectedStart));
        }

        [Then(@"the lenght of the SMS must be registered to be ""(.*)"" characters")]
        public void ThenTheLenghtOfTheSMSMustBeRegisteredToBe128Characters(string expectedLenght)
        {
            var sms = ScenarioContext.Current.Get<SMSServiceCall>();
            sms.NoOfCharacters.Should().Be(int.Parse(expectedLenght));
        }

        [Then(@"the receiver of the SMS must be registered as ""(.*)""")]
        public void ThenTheReceiverOfTheSMSMustBeRegisteredAs98561234(string receiver)
        {
            var sms = ScenarioContext.Current.Get<SMSServiceCall>();
            sms.DestinationPhoneNumber.Should().Be(receiver);
        }

        [Then(@"the country from which the SMS was sent from must be registered as ""(.*)""")]
        public void ThenTheCountryFromWhichTheSMSWasSentFromMustBeRegisteredAsDK(string country)
        {
            var sms = ScenarioContext.Current.Get<SMSServiceCall>();
            sms.FromCountry.Should().Be(country);
        }

        [Then(@"the country for which the SMS was sent to must be registered as ""(.*)""")]
        public void ThenTheCountryForWhichTheSMSWasSentToMustBeRegisteredAsDE(string country)
        {
            var sms = ScenarioContext.Current.Get<SMSServiceCall>();
            sms.ToCountry.Should().Be(country);
        }

        [Given(@"the subscription includes support for texting from country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTextingFromCountryDE(string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber, country));
        }

        [Given(@"the subscription includes support for texting to country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTextingToCountryDK(string country)
        {
            _subscriptionService.AddServiceChargeToSubscription(ChargeHelper.CreateStandardFixedCharge(_subscription.PhoneNumber, country));
        }
    }
}

﻿using System;
using System.Linq;
using CallServices.Calls;
using Core;
using Core.Models;
using FluentAssertions;
using SubscriptionServices;
using SubscriptionServices.ServiceCharges;
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
            _subscription =SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(_subscriptionRegistration, _customerRegistration,"66665555", "DK", CustomerStatus.Normal);

            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 1, "Standard Fixed Fee", "DK"));
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
            _callRegistration.RegisterACall(new SMSCall(  _subscription.PhoneNumber,
                                                            DateTime.Parse(startTime),
                                                            _smsLenght,
                                                            _receiver,
                                                            _fromCountry,
                                                            _toCountry));
        }

        [Then(@"I must be able to find the SMS using the subscription")]
        public void ThenIMustBeAbleToFindTheSMSUsingTheSubscription()
        {
            var calls = _callSearch.GetCallsMadeFromPhoneNumber(_subscription.PhoneNumber);

            calls.Count().Should().Be(1);

            ScenarioContext.Current.Set(calls.First() as SMSCall); 
        }

        [Then(@"the start time of the SMS must be registered at ""(.*)""")]
        public void ThenTheStartTimeOfTheSMSMustBeRegisteredAt090000(string expectedStart)
        {
            var sms = ScenarioContext.Current.Get<SMSCall>();
            sms.SendTime.Should().Be(DateTime.Parse(expectedStart));
        }

        [Then(@"the lenght of the SMS must be registered to be ""(.*)"" characters")]
        public void ThenTheLenghtOfTheSMSMustBeRegisteredToBe128Characters(string expectedLenght)
        {
            var sms = ScenarioContext.Current.Get<SMSCall>();
            sms.NoOfCharacters.Should().Be(int.Parse(expectedLenght));
        }

        [Then(@"the receiver of the SMS must be registered as ""(.*)""")]
        public void ThenTheReceiverOfTheSMSMustBeRegisteredAs98561234(string receiver)
        {
            var sms = ScenarioContext.Current.Get<SMSCall>();
            sms.DestinationPhoneNumber.Should().Be(receiver);
        }

        [Then(@"the country from which the SMS was sent from must be registered as ""(.*)""")]
        public void ThenTheCountryFromWhichTheSMSWasSentFromMustBeRegisteredAsDK(string country)
        {
            var sms = ScenarioContext.Current.Get<SMSCall>();
            sms.FromCountry.Should().Be(country);
        }

        [Then(@"the country for which the SMS was sent to must be registered as ""(.*)""")]
        public void ThenTheCountryForWhichTheSMSWasSentToMustBeRegisteredAsDE(string country)
        {
            var sms = ScenarioContext.Current.Get<SMSCall>();
            sms.ToCountry.Should().Be(country);
        }

        [Given(@"the subscription includes support for texting from country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTextingFromCountryDE(string country)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 1, "Standard Fixed Fee", country));
        }

        [Given(@"the subscription includes support for texting to country: ""(.*)""")]
        public void GivenTheSubscriptionIncludesSupportForTextingToCountryDK(string country)
        {
            _serviceChargeRegistration.AddServiceChargeToSubscription(new FixedCharge(_subscription.PhoneNumber, ServiceType.SMS, 1, "Standard Fixed Fee", country));
        }
    }
}

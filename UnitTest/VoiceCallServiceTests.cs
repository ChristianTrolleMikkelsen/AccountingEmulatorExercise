using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Services;

namespace UnitTest
{
    [TestFixture]
    class VoiceCallServiceTests
    {
        [Test]
        public void Should_Support_VoiceCall_Calls()
        {
            var service = new VoiceCallService("");

            service.HasSupportForCall(new VoiceCall("", DateTime.Now, DateTime.Now.TimeOfDay, "", "", ""))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls()
        {
            var service = new VoiceCallService("");

            service.HasSupportForCall(new DummyCall())
                .Should().BeFalse();
        }

        internal class DummyCall : ICall
        {
            public string SourcePhoneNumber
            {
                get { return "I Am A Dummy"; }
            }
        }
    }
}

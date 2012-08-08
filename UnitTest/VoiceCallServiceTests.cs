using System;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;
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

            service.HasSupportForCall(new VoiceServiceCall("", DateTime.Now, DateTime.Now.TimeOfDay, "", "", ""))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls()
        {
            var service = new VoiceCallService("");

            service.HasSupportForCall(new DummyServiceCall())
                .Should().BeFalse();
        }

        internal class DummyServiceCall : IServiceCall
        {
            public string PhoneNumber
            {
                get { return "I Am A Dummy"; }
            }
        }

        [Test]
        public void Should_Support_VoiceCall_Charges()
        {
            var service = new VoiceCallService("");

            service.HasSupportForCharge(new VoiceCallSecondCharge("", 1.1M))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Charges()
        {
            var service = new VoiceCallService("");

            service.HasSupportForCharge(new DummyCharge())
                .Should().BeFalse();
        }

        internal class DummyCharge : IServiceCharge
        {
            public string PhoneNumber
            {
                get { return "I Am A Dummy"; }
            }

            public Record GenerateBill(IServiceCall call)
            {
                throw new NotImplementedException();
            }
        }
    }
}

using System;

using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.ServiceCharges.Voice;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class VoiceCallServiceTests
    {
        [Test]
        public void Should_Support_VoiceCall_Calls()
        {
            var service = new VoiceService("");

            service.HasSupportForCall(new VoiceServiceCall("", DateTime.Now, DateTime.Now.TimeOfDay, "", "", ""))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls()
        {
            var service = new VoiceService("");

            service.HasSupportForCall(new DummyServiceCall())
                .Should().BeFalse();
        }

        internal class DummyServiceCall : IServiceCall
        {
            public string PhoneNumber
            {
                get { return "I Am A Dummy"; }
            }

            public string FromCountry
            {
                get { throw new NotImplementedException(); }
            }

            public string ToCountry
            {
                get { throw new NotImplementedException(); }
            }
        }

        [Test]
        public void Should_Support_VoiceCall_Charges()
        {
            var service = new VoiceService("");

            service.HasSupportForCharge(new SecondCharge("", 1.1M))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Charges()
        {
            var service = new VoiceService("");

            service.HasSupportForCharge(new DummyCharge())
                .Should().BeFalse();
        }

        internal class DummyCharge : IServiceCharge
        {
            public string PhoneNumber
            {
                get { return "I Am A Dummy"; }
            }

            public decimal CalculateCharge(IServiceCall call)
            {
                throw new NotImplementedException();
            }

            public decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : class
            {
                throw new NotImplementedException();
            }
        }
    }
}

using System;
using CallCentral.Calls;
using Core;
using Core.ServiceCalls;
using Core.ServiceCharges;
using FluentAssertions;
using NUnit.Framework;
using SubscriptionService.ServiceCharges;

namespace UnitTest
{
    [TestFixture]
    class FixedChargeTest
    {
        [Test]
        public void Must_Return_Raw_Chrage_Value()
        {
            var fixedCharge = new FixedCharge("99999999", ServiceType.Voice, 123, "Fixed Charge Test", "DK");

            fixedCharge.CalculateCharge(new VoiceCall("99999999",
                                                             DateTime.Now,
                                                             new TimeSpan(0, 0, 4, 36),
                                                             "00000000",
                                                             "DK",
                                                             "DK")).Should().Be(123);
        }
    }
}

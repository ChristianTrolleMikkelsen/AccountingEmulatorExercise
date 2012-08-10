using System;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class FixedChargeTest
    {
        [Test]
        public void Must_Return_Raw_Chrage_Value()
        {
            var fixedCharge = new FixedCharge("99999999", typeof (VoiceService), 123, "Fixed Charge Test", "DK");

            fixedCharge.CalculateCharge(new VoiceServiceCall("99999999",
                                                             DateTime.Now,
                                                             new TimeSpan(0, 0, 4, 36),
                                                             "00000000",
                                                             "DK",
                                                             "DK")).Should().Be(123);
        }
    }
}

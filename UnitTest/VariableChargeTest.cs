using System;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class VariableChargeTest
    {
        [Test]
        public void Must_Return_Calculated_Value_When_Standard_Data_Is_Supplied()
        {
            var fixedCharge = new VariableCharge("99999999", typeof(VoiceService), 100, 10 , "Variable Charge Test", "DK");

            fixedCharge.CalculateCharge(new VoiceServiceCall("99999999",
                                                             DateTime.Now,
                                                             new TimeSpan(0, 0, 4, 0),
                                                             "00000000",
                                                             "DK",
                                                             "DK"))
                                                             .Should().Be(100*((4*60)/10));
        }

        [Test]
        public void Must_Return_Handle_Zero_Duration()
        {
            var fixedCharge = new VariableCharge("99999999", typeof(VoiceService), 100, 10, "Variable Charge Test", "DK");

            fixedCharge.CalculateCharge(new VoiceServiceCall("99999999",
                                                             DateTime.Now,
                                                             new TimeSpan(0, 0, 0, 0),
                                                             "00000000",
                                                             "DK",
                                                             "DK"))
                                                             .Should().Be(100 * ((0 * 60) / 10));
        }

        [Test]
        public void Must_Be_Handle_Handle_Charge_Pr_Unit()
        {
            var fixedCharge = new VariableCharge("99999999", typeof(VoiceService), 0, 10, "Variable Charge Test", "DK");

            fixedCharge.CalculateCharge(new VoiceServiceCall("99999999",
                                                             DateTime.Now,
                                                             new TimeSpan(0, 0, 4, 0),
                                                             "00000000",
                                                             "DK",
                                                             "DK"))
                                                             .Should().Be(0 * ((4 * 60) / 10));
        }
        [Test]
        public void Must_Throw_Exception_If_Duration_Is_Zero()
        {
            Assert.Throws<Exception>(delegate { new VariableCharge("99999999", typeof (VoiceService), 100, 0, "Variable Charge Test", "DK"); });
        }
    }
}

using System;
using Core.ServiceCalls;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class ServiceCallTests
    {
        [Test]
        public void DataTransferCalls_Must_Use_Size_As_UnitSize()
        {
            var call = new DataTransferCall("", new DateTime(2012, 1, 1), 123, "", "", "");

            call.GetUnitSize().Should().Be(123);
        }

        [Test]
        public void DataTransferCalls_Must_Use_TransferStart_As_StartTime()
        {
            var call = new DataTransferCall("", new DateTime(2012, 1, 1), 123, "", "", "");

            call.GetStartTime().Should().Be(new DateTime(2012, 1, 1));
        }

        [Test]
        public void SMSCalls_Must_Use_Length_As_UnitSize()
        {
            var call = new SMSServiceCall("", new DateTime(2012, 1, 1), 123, "", "", "");

            call.GetUnitSize().Should().Be(123);
        }

        [Test]
        public void SMSCalls_Must_Use_SendTime_As_StartTime()
        {
            var call = new SMSServiceCall("", new DateTime(2012, 1, 1), 123, "", "", "");

            call.GetStartTime().Should().Be(new DateTime(2012, 1, 1));
        }

        [Test]
        public void VoiceCalls_Must_Use_Duration_In_Seconds_As_UnitSize()
        {
            var call = new VoiceServiceCall("", new DateTime(2012, 1, 1), new TimeSpan(0,0,1,2), "", "", "");

            call.GetUnitSize().Should().Be(62);
        }

        [Test]
        public void VoiceCalls_Must_Use_Start_As_StartTime()
        {
            var call = new VoiceServiceCall("", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 1, 2), "", "", "");

            call.GetStartTime().Should().Be(new DateTime(2012, 1, 1));
        }

        [Test]
        public void Call_Must_Be_Invalid_If_There_Is_No_PhoneNumber()
        {
            var call = new VoiceServiceCall("", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 1, 2), "DEST", "DK", "DK");
            call.IsValid().Should().BeFalse();
        }

        [Test]
        public void Call_Must_Be_Invalid_If_There_Is_No_From_Country()
        {
            var call = new VoiceServiceCall("99999999", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 1, 2), "DEST", "", "DK");
            call.IsValid().Should().BeFalse();
        }

        [Test]
        public void Call_Must_Be_Invalid_If_There_Is_No_To_Country()
        {
            var call = new VoiceServiceCall("99999999", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 1, 2), "DEST", "DK", "");
            call.IsValid().Should().BeFalse();
        }

        [Test]
        public void Call_Must_Be_Invalid_If_There_Is_No_Destination()
        {
            var call = new VoiceServiceCall("99999999", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 1, 2), "", "DK", "DK");
            call.IsValid().Should().BeFalse();
        }

        [Test]
        public void Call_Must_Be_Invalid_If_There_Is_No_UnitSize()
        {
            var call = new VoiceServiceCall("99999999", new DateTime(2012, 1, 1), new TimeSpan(0, 0, 0, 0), "DEST", "DK", "DK");
            call.IsValid().Should().BeFalse();
        }
    }
}

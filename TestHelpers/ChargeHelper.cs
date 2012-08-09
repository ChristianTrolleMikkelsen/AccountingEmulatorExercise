using Core.ServiceCharges;
using Core.Services;

namespace TestHelpers
{
    public class ChargeHelper
    {
        public static readonly decimal SecondUnitSize = 1;
        public static readonly decimal MinuteUnitSize = 60;

        public static FixedCharge CreateStandardFixedCharge(string phoneNumber)
        {
            return new FixedCharge(phoneNumber, typeof(VoiceService), 1, "Standard Fixed Fee");
        }
    }
}

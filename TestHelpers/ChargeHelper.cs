using Core;
using Core.ServiceCharges;
using Core.Services;

namespace TestHelpers
{
    public class ChargeHelper
    {
        public static readonly decimal SecondUnitSize = 1;
        public static readonly decimal MinuteUnitSize = 60;
        public static readonly decimal OneKiloByteUnitSize = 1024M;
        public static readonly decimal OneMegaByteUnitSize = 1024M * 1024M;

        public static FixedCharge CreateStandardFixedCharge(string phoneNumber)
        {
            return new FixedCharge(phoneNumber, ServiceType.Voice, 1, "Standard Fixed Fee", "DK");
        }

        public static FixedCharge CreateStandardFixedCharge(string phoneNumber, string countryIsoCode)
        {
            return new FixedCharge(phoneNumber, ServiceType.Voice, 1, "Standard Fixed Fee", countryIsoCode);
        }
    }
}

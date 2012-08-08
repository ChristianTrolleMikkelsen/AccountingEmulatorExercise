using System;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public class VoiceCallSecondCharge : IVoiceCallCharge
    {
        private readonly decimal _chargePrSecond;
        public string PhoneNumber { get; private set; }

        public VoiceCallSecondCharge(string phoneNumber, decimal chargePrSecond)
        {
            _chargePrSecond = chargePrSecond;
            PhoneNumber = phoneNumber;
        }

        public Record GenerateBill(IServiceCall call)
        {
            if (call is VoiceServiceCall == false)
            {
                throw new Exception("");
            }

            return new Record(call.PhoneNumber,
                                "",
                                CalculateCharge(call as VoiceServiceCall));
        }

        private decimal CalculateCharge(VoiceServiceCall call)
        {
            return Convert.ToDecimal(call.Duration.TotalSeconds) * _chargePrSecond;
        }
    }
}

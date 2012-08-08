using System;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public class VoiceCallMinuteCharge : VoiceCallCharge
    {
        public VoiceCallMinuteCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var voiceCall = ConvertToVoiceCall(call);

            var totalMinutesAsDecimal = Convert.ToDecimal(voiceCall.Duration.TotalMinutes);
            return Math.Ceiling(totalMinutesAsDecimal) * _charge;
        }

    }
}

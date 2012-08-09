using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges.Voice
{
    public class MinuteCharge : VoiceServiceCharge
    {
        public MinuteCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var voiceCall = ConvertToVoiceCall(call);

            var totalMinutesAsDecimal = Convert.ToDecimal(voiceCall.Duration.TotalMinutes);
            return Math.Ceiling(totalMinutesAsDecimal) * _charge;
        }
    }
}

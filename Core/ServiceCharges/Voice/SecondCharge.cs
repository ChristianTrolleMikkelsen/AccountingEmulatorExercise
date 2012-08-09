using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges.Voice
{
    public class SecondCharge : VoiceServiceCharge
    {
        public SecondCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var voiceCall = ConvertToVoiceCall(call);

            return Convert.ToDecimal(voiceCall.Duration.TotalSeconds) * _charge;
        }
    }
}

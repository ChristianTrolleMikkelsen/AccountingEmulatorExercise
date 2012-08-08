using System;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public class VoiceCallIntervalCharge : VoiceCallCharge
    {
        private readonly TimeSpan _interval;

        public VoiceCallIntervalCharge(string phoneNumber, decimal charge, TimeSpan interval)
            : base(phoneNumber, charge)
        {
            _interval = interval;
        }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var voiceCall = ConvertToVoiceCall(call);

            var noOfIntervalsBegun = Convert.ToDecimal(voiceCall.Duration.Ticks) / Convert.ToDecimal(_interval.Ticks);

            return Math.Ceiling(noOfIntervalsBegun) * _charge;
        }
    }
}

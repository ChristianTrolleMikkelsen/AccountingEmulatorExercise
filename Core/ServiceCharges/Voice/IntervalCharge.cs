using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges.Voice
{
    public class IntervalCharge : VoiceServiceCharge
    {
        private readonly TimeSpan _interval;

        public IntervalCharge(string phoneNumber, decimal charge, TimeSpan interval)
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

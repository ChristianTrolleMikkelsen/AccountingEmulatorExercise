﻿using System;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public class VoiceCallSecondCharge : VoiceCallCharge
    {
        public VoiceCallSecondCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var voiceCall = ConvertToVoiceCall(call);

            return Convert.ToDecimal(voiceCall.Duration.TotalSeconds) * _charge;
        }
    }
}
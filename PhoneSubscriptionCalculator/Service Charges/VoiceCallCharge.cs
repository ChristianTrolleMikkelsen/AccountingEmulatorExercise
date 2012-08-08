using System;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public interface IVoiceCallCharge : IServiceCharge
    {

    }

    public abstract class VoiceCallCharge : IVoiceCallCharge
    {
        protected readonly decimal _charge;
        public string PhoneNumber { get; private set; }

        public VoiceCallCharge(string phoneNumber, decimal charge)
        {
            _charge = charge;
            PhoneNumber = phoneNumber;
        }

        public virtual decimal CalculateCharge(IServiceCall call)
        {
            throw new NotImplementedException();
        }

        protected VoiceServiceCall ConvertToVoiceCall(IServiceCall call)
        {
            return call as VoiceServiceCall;
        }
    }
}

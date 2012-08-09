using Core.ServiceCalls;

namespace Core.ServiceCharges.Voice
{
    public class CallCharge : VoiceServiceCharge
    {
        public CallCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            return  _charge;
        }
    }
}

using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public class VoiceCallCallCharge : VoiceCallCharge
    {
        public VoiceCallCallCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge) { }

        public override decimal CalculateCharge(IServiceCall call)
        {
            return  _charge;
        }
    }
}

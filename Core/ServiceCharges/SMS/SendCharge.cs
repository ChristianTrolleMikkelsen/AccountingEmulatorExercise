using Core.ServiceCalls;

namespace Core.ServiceCharges.SMS
{
    public class SendCharge : SMSServiceCharge
    {
        public SendCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge)
        {
        }

        public override decimal CalculateCharge(IServiceCall call)
        {
            return _charge;
        }
    }
}

using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;

namespace PhoneSubscriptionCalculator.Services
{
    public class VoiceCallService : IService
    {
        public string PhoneNumber { get; private set; }

        public VoiceCallService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(IServiceCall serviceCall)
        {
            return serviceCall.GetType() == typeof(VoiceServiceCall);
        }

        public bool HasSupportForCharge(IServiceCharge charge)
        {
            return charge is IVoiceCallCharge;
        }
    }
}

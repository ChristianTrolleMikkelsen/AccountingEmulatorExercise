using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Services
{
    public class VoiceCallService : IService
    {
        public string PhoneNumber { get; private set; }

        public VoiceCallService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(ICall call)
        {
            return call.GetType() == typeof(VoiceCall);
        }
    }
}

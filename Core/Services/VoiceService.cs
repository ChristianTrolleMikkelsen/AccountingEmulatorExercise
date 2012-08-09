using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.ServiceCharges.Voice;

namespace Core.Services
{
    public class VoiceService : IService
    {
        public string PhoneNumber { get; private set; }

        public VoiceService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(IServiceCall serviceCall)
        {
            return serviceCall.GetType() == typeof(VoiceServiceCall);
        }

        public bool HasSupportForCharge(IServiceCharge charge)
        {
            return charge is IVoiceServiceCharge;
        }
    }
}

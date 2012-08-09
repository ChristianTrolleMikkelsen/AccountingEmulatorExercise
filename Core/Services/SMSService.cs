using Core.ServiceCalls;

namespace Core.Services
{
    public class SMSService : IService
    {
        public string PhoneNumber { get; private set; }

        public SMSService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(IServiceCall serviceCall)
        {
            return serviceCall.GetType() == typeof(SMSServiceCall);
        }
    }
}

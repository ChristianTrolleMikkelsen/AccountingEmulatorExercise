using Core.ServiceCalls;

namespace Core.Services
{
    public class DataTransferService : IService
    {
        public string PhoneNumber { get; private set; }

        public DataTransferService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(IServiceCall serviceCall)
        {
            return serviceCall.GetType() == typeof(DataTransferCall);
        }
    }
}

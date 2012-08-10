using Core.ServiceCalls;

namespace Core.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCallType(ServiceCallType serviceCall);
        ServiceType Type { get; }
    }
}

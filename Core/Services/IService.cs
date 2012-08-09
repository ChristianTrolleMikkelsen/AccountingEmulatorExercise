using Core.ServiceCalls;

namespace Core.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCall(IServiceCall serviceCall);
    }
}

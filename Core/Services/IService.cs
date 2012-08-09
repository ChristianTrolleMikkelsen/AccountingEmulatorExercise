using Core.ServiceCalls;
using Core.ServiceCharges;

namespace Core.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCall(IServiceCall serviceCall);
        bool HasSupportForCharge(IServiceCharge charge);
    }
}

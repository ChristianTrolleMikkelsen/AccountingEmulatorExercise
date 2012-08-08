using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCall(ICall call);
    }
}

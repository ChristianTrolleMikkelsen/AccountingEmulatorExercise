using PhoneSubscriptionCalculator.Service_Calls;
using PhoneSubscriptionCalculator.Service_Charges;

namespace PhoneSubscriptionCalculator.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCall(IServiceCall serviceCall);
        bool HasSupportForCharge(IServiceCharge charge);
    }
}

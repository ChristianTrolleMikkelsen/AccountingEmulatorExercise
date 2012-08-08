using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Service_Calls;

namespace PhoneSubscriptionCalculator.Service_Charges
{
    public interface IServiceCharge
    {
        string PhoneNumber { get; }
        Record GenerateBill(IServiceCall call);
    }
}

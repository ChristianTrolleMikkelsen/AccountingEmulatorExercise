using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public interface IServiceCharge
    {
        string PhoneNumber { get; }
        string Country { get; }
        string Description { get; }
        ServiceType ServiceType { get; }
        decimal ChargePrUnit { get; }

        decimal CalculateCharge(IServiceCall call);
    }
}

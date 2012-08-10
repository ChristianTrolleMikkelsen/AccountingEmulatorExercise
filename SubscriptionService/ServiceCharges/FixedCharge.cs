using Core;
using Core.ServiceCalls;

namespace SubscriptionService.ServiceCharges
{
    public class FixedCharge : BaseCharge
    {
        public FixedCharge(string phoneNumber, ServiceType typeOfService, decimal chargePrUnit, string description, string country)
            : base(phoneNumber, typeOfService, chargePrUnit, description, country)
        {
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            return ChargePrUnit;
        }
    }
}

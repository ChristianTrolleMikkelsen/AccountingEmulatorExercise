using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public class FixedCharge : ServiceCharge
    {
        public FixedCharge(string phoneNumber, Type typeOfService, decimal chargePrUnit, string description)
            : base(phoneNumber, typeOfService, chargePrUnit, description)
        {
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            return ChargePrUnit;
        }
    }
}

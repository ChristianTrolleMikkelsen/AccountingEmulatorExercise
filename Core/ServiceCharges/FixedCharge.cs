using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public class FixedCharge : BaseCharge
    {
        public FixedCharge(string phoneNumber, Type typeOfService, decimal chargePrUnit, string description, string country)
            : base(phoneNumber, typeOfService, chargePrUnit, description, country)
        {
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            return ChargePrUnit;
        }
    }
}

using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public class IntervalCharge : ServiceCharge
    {
        public decimal IntervalSize { get; private set; }

        public IntervalCharge(string phoneNumber, Type typeOfService, decimal chargePrUnit, decimal intervalSize, string description)
            : base(phoneNumber, typeOfService, chargePrUnit, description)
        {
            IntervalSize = intervalSize;
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            return serviceCall.GetUnitSize() / IntervalSize * ChargePrUnit;
        }
    }
}

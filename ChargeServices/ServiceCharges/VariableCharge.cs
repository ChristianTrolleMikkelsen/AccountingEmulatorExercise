using System;
using Core;
using Core.ServiceCalls;

namespace ChargeServices.ServiceCharges
{
    public class VariableCharge : BaseCharge
    {
        public decimal UnitSize { get; private set; }

        public VariableCharge(string phoneNumber, ServiceType typeOfService, decimal chargePrUnit, decimal unitSize, string description, string country)
            : base(phoneNumber, typeOfService, chargePrUnit, description, country)
        {
            UnitSize = unitSize;
            EvaluateUnitSize();
        }

        private void EvaluateUnitSize()
        {
            if(UnitSize == 0M)
            {
                throw new Exception(string.Format("UnitSize may not be Zero for charge. PhoneNumber: {0} Service: {1} Country: {2} Description: {3}", PhoneNumber, ServiceType, Country,Description));
            }
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            var division = serviceCall.GetUnitSize() / UnitSize;
            return Math.Ceiling(division) * ChargePrUnit;
        }
    }
}

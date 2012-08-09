﻿using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public class VariableCharge : ServiceCharge
    {
        public decimal UnitSize { get; private set; }

        public VariableCharge(string phoneNumber, Type typeOfService, decimal chargePrUnit, decimal unitSize, string description, string country)
            : base(phoneNumber, typeOfService, chargePrUnit, description, country)
        {
            UnitSize = unitSize;
        }

        public override decimal CalculateCharge(IServiceCall serviceCall)
        {
            var division = serviceCall.GetUnitSize() / UnitSize;
            return Math.Ceiling(division) * ChargePrUnit;
        }
    }
}

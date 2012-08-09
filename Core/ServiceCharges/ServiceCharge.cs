using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public interface IServiceCharge
    {
        string Description { get;  }
        string PhoneNumber { get; }
        Type ServiceType { get; }
        decimal ChargePrUnit { get; }

        decimal CalculateCharge(IServiceCall call);
    }

    public abstract class ServiceCharge : IServiceCharge
    {
        public string Description { get; private set; }
        public string PhoneNumber { get; private set; }
        public Type ServiceType { get; private set; }
        public decimal ChargePrUnit { get; private set; }

        protected ServiceCharge(string phoneNumber, Type typeOfService, decimal chargePrUnit, string description)
        {
            Description = description;
            PhoneNumber = phoneNumber;
            ServiceType = typeOfService;
            ChargePrUnit = chargePrUnit;
        }

        public virtual decimal CalculateCharge(IServiceCall call)
        {
            throw new NotImplementedException();
        }
    }
}

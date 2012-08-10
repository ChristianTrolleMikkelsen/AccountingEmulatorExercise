using Core.ServiceCalls;
using Core.Services;

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

    public abstract class BaseCharge : IServiceCharge
    {
        public string Description { get; private set; }
        public string PhoneNumber { get; private set; }
        public ServiceType ServiceType { get; private set; }
        public string Country { get; private set; }
        public decimal ChargePrUnit { get; private set; }

        protected BaseCharge(string phoneNumber, ServiceType typeOfService, decimal chargePrUnit, string description, string country)
        {
            Country = country;
            Description = string.Format("({0}) {1}",country, description);
            PhoneNumber = phoneNumber;
            ServiceType = typeOfService;
            ChargePrUnit = chargePrUnit;
        }

        public abstract decimal CalculateCharge(IServiceCall call);
    }
}

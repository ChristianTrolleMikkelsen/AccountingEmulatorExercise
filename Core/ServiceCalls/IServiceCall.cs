using System;

namespace Core.ServiceCalls
{
    public interface IServiceCall
    {
        string PhoneNumber { get; }
        string FromCountry { get; }
        string ToCountry { get; }
        ServiceType Type { get; }
        DateTime GetStartTime();
        decimal GetUnitSize();
        bool IsValid();
    }
}

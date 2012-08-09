using System;

namespace Core.ServiceCalls
{
    public interface IServiceCall
    {
        string PhoneNumber { get; }
        string FromCountry { get; }
        string ToCountry { get; }
        DateTime GetStartTime();
        decimal GetUnitSize();
    }
}

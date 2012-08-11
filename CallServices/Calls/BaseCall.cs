using System;
using Core.ServiceCalls;

namespace CallServices.Calls
{
    public abstract class BaseCall : IServiceCall
    {
        public ServiceCallType Type { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        protected BaseCall(string phoneNumber, string fromCountry, string toCountry, ServiceCallType callType)
        {
            Type = callType;
            PhoneNumber = phoneNumber;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }

        public abstract DateTime GetStartTime();

        public abstract decimal GetUnitSize();

        protected abstract string GetDestination();

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(PhoneNumber)
                   && !string.IsNullOrEmpty(FromCountry)
                   && !string.IsNullOrEmpty(ToCountry)
                   && !string.IsNullOrEmpty(GetDestination())
                   && !(GetUnitSize() <= 0);
        }
    }
}

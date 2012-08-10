using System;
using Core;
using Core.ServiceCalls;

namespace SubscriptionService.Services
{
    public interface IService
    {
        string PhoneNumber { get; }
        bool HasSupportForCallType(ServiceCallType serviceCall);
        ServiceType Type { get; }
    }

    public class Service : IService
    {
        public string PhoneNumber { get; private set; }
        public ServiceType Type { get; private set; }

        public Service(string phoneNumber, ServiceType serviceType)
        {
            PhoneNumber = phoneNumber;
            Type = serviceType;
        }

        public bool HasSupportForCallType(ServiceCallType serviceCallType)
        {
            switch (serviceCallType)
            {
                case ServiceCallType.Voice:
                    return Type == ServiceType.Voice;
                case ServiceCallType.SMS:
                    return Type == ServiceType.SMS;
                case ServiceCallType.DataTransfer:
                    return Type == ServiceType.DataTransfer;
                default:
                    throw new Exception("Unknown Service Type");
            }
        }
    }
}

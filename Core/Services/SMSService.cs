using System.Collections.Generic;
using Core.ServiceCalls;
using Core.ServiceCharges;
using Core.ServiceCharges.SMS;
using Core.ServiceCharges.Voice;

namespace Core.Services
{
    /*public abstract class Service : IService
    {
        public string PhoneNumber { get; private set; }

        public Service(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool Supports(IServiceCall call, IServiceCharge charge)
        {
            return (SupportsCall(call) && SupportsCharge(charge));
        }

        protected virtual bool SupportsCharge(IServiceCharge charge)
        {
            return true;
        }

        protected virtual bool SupportsCall(IServiceCall call)
        {
            return true;
        }

        public decimal CalculateCharge(VoiceServiceCall call)
        {
            
        }
    }*/

    public class SMSService : IService
    {
        public string PhoneNumber { get; private set; }

        public SMSService(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool HasSupportForCall(IServiceCall serviceCall)
        {
            return serviceCall.GetType() == typeof(SMSServiceCall);
        }

        public bool HasSupportForCharge(IServiceCharge charge)
        {
            return charge is ISMSServiceCharge;
        }
    }
}

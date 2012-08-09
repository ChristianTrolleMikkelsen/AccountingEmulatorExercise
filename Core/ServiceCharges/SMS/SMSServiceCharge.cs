using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ServiceCalls;

namespace Core.ServiceCharges.SMS
{
    public interface ISMSServiceCharge : IServiceCharge
    {

    }

    public abstract class SMSServiceCharge : ISMSServiceCharge
    {
        protected readonly decimal _charge;
        public string PhoneNumber { get; private set; }

        public SMSServiceCharge(string phoneNumber, decimal charge)
        {
            _charge = charge;
            PhoneNumber = phoneNumber;
        }

        public virtual decimal CalculateCharge(IServiceCall call)
        {
            throw new NotImplementedException();
        }

       /* public decimal CalculateCharge<T>(T call)
        {
            throw new NotImplementedException();
        }*/

        /*  public decimal CalculateCharge<T>(T call) where T : SMSServiceCall
        {
            throw new NotImplementedException();
        }

        public decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : class
        {
            throw new NotImplementedException();
        }*/

        protected SMSServiceCall ConvertToSMSCall(IServiceCall call)
        {
            return call as SMSServiceCall;
        }
    }
}

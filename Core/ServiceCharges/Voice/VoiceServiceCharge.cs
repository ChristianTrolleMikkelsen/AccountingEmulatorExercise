using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges.Voice
{
    public interface IVoiceServiceCharge : IServiceCharge
    {

    }

    public abstract class VoiceServiceCharge : IVoiceServiceCharge
    {
        protected readonly decimal _charge;
        public string PhoneNumber { get; private set; }

        public VoiceServiceCharge(string phoneNumber, decimal charge)
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

     /*   public decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : class
        {
            throw new NotImplementedException();
        }

        public T ConvertServiceCallTo<T>(IServiceCall call, T typeToConvertTo) where T : class
        {
            return call as T;
        }*/

        protected VoiceServiceCall ConvertToVoiceCall(IServiceCall call)
        {
            return call as VoiceServiceCall;
        }
    }
}

using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges
{
    public interface IServiceCharge
    {
        string PhoneNumber { get; }
        decimal CalculateCharge(IServiceCall call);
/*        decimal CalculateCharge<T>(T call);
                decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : class;
                decimal CalculateCharge<T>(T call) where T : class;
                decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : IServiceCall;*/
    }

    public abstract class ServiceCharge : IServiceCharge
    {
        protected readonly decimal _charge;
        public string PhoneNumber { get; private set; }

        public ServiceCharge(string phoneNumber, decimal charge)
        {
            _charge = charge;
            PhoneNumber = phoneNumber;
        }

        public decimal CalculateCharge(IServiceCall call)
        {
            throw new NotImplementedException();
        }

      /*  public decimal CalculateCharge<T>(T call)
        {
            throw new NotImplementedException();
        }*/

        /*     public decimal CalculateCharge<T>(IServiceCall call, T typeOfCall) where T : class
        {
            var type = call as T;

//            var type = ConvertServiceCallTo<T>(call);

            return 0;
        }

        private T ConvertServiceCallTo<T>(IServiceCall call) where T : class
        {
            return call as T;
        }

        protected virtual decimal Calculate<T>(T type)
        {
            throw new NotImplementedException();
        }*/
    }
}

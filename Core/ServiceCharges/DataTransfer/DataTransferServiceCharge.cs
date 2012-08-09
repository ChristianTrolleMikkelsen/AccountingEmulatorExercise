using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ServiceCalls;

namespace Core.ServiceCharges.SMS
{
    public interface IDataTransferServiceCharge : IServiceCharge
    {

    }

    public abstract class DataTransferServiceCharge : IDataTransferServiceCharge
    {
        protected readonly decimal _charge;
        public string PhoneNumber { get; private set; }

        public DataTransferServiceCharge(string phoneNumber, decimal charge)
        {
            _charge = charge;
            PhoneNumber = phoneNumber;
        }

        public virtual decimal CalculateCharge(IServiceCall call)
        {
            throw new NotImplementedException();
        }

        protected DataTransferCall ConvertToDataTransferCall(IServiceCall call)
        {
            return call as DataTransferCall;
        }
    }
}

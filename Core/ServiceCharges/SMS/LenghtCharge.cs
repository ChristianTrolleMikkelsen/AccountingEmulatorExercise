using System;
using Core.ServiceCalls;

namespace Core.ServiceCharges.SMS
{
    public class LenghtCharge : SMSServiceCharge
    {
        private readonly int _chunkLength;

        public LenghtCharge(string phoneNumber, decimal charge, int chunkLength) : base(phoneNumber, charge)
        {
            _chunkLength = chunkLength;
        }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var smsCall = ConvertToSMSCall(call);

            var numberOfChunks = Math.Round(Convert.ToDecimal(smsCall.Lenght) / _chunkLength);
            return (numberOfChunks) * _charge;
        }

      /*  public decimal CalculateCharge<T>(T call) where T : SMSServiceCall
        {
            var type = ConvertToSMSCall(call);

            var totalMinutesAsDecimal = Convert.ToDecimal(call.Lenght);
            return Math.Ceiling(totalMinutesAsDecimal) * _charge;
        }*/
    }
}

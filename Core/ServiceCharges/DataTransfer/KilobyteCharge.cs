using Core.ServiceCalls;

namespace Core.ServiceCharges.SMS
{
    public class KilobyteCharge : DataTransferServiceCharge
    {
        public KilobyteCharge(string phoneNumber, decimal charge)
            : base(phoneNumber, charge)
        {
        }

        public override decimal CalculateCharge(IServiceCall call)
        {
            var dataCall = ConvertToDataTransferCall(call);

            return dataCall.Size*_charge;
        }
    }
}

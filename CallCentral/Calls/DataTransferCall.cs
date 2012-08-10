using System;
using Core.ServiceCalls;

namespace CallCentral.Calls
{
    public class DataTransferCall : BaseCall
    {
        public DateTime TransferStart { get; private set; }
        public int Size { get; private set; }
        public string TransferUrl { get; private set; }

        public DataTransferCall(string phoneNumber, DateTime transferStart, int size, string transferUrl, string fromCountry, string toCountry)
            : base(phoneNumber, fromCountry, toCountry, ServiceCallType.DataTransfer)
        {
            TransferStart = transferStart;
            Size = size;
            TransferUrl = transferUrl;
        }

        public override DateTime GetStartTime()
        {
            return TransferStart;
        }

        public override decimal GetUnitSize()
        {
            return Size;
        }

        protected override string GetDestination()
        {
            return TransferUrl;
        }

        public override string ToString()
        {
            return string.Format("DataTransfer {0}, {1} bytes.", TransferUrl, Size);
        }
    }
}

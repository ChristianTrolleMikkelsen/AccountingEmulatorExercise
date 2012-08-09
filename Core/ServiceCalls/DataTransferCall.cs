using System;

namespace Core.ServiceCalls
{
    public class DataTransferCall : IServiceCall
    {
        public DateTime TransferStart { get; private set; }
        public int Size { get; private set; }
        public string PhoneNumber { get; private set; }
        public string TransferUrl { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public DataTransferCall(string sourcePhoneNumber, DateTime transferStart, int size, string transferUrl, string fromCountry, string toCountry)
        {
            TransferStart = transferStart;
            Size = size;
            PhoneNumber = sourcePhoneNumber;
            TransferUrl = transferUrl;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }

        public DateTime GetStartTime()
        {
            return TransferStart;
        }

        public decimal GetUnitSize()
        {
            return Size;
        }

        public override string ToString()
        {
            return string.Format("DataTransfer {0}, {1} bytes.", TransferUrl, Size);
        }
    }
}

using System;

namespace Core.ServiceCalls
{
    public class DataTransferCall : IServiceCall
    {
        public DateTime Start { get; private set; }
        public int Size { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Url { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public DataTransferCall(string sourcePhoneNumber, DateTime start, int size, string url, string fromCountry, string toCountry)
        {
            Start = start;
            Size = size;
            PhoneNumber = sourcePhoneNumber;
            Url = url;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }
    }
}

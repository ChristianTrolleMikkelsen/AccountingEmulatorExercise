using System;

namespace Core.ServiceCalls
{
    public class SMSServiceCall : IServiceCall
    {
        public DateTime Start { get; private set; }
        public int Lenght { get; private set; }
        public string PhoneNumber { get; private set; }
        public string DestinationPhoneNumber { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public SMSServiceCall(string sourcePhoneNumber, DateTime start, int lenght, string destinationPhoneNumber, string fromCountry, string toCountry)
        {
            Start = start;
            Lenght = lenght;
            PhoneNumber = sourcePhoneNumber;
            DestinationPhoneNumber = destinationPhoneNumber;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }
    }
}

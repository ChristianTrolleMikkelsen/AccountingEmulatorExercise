using System;
using Core.ServiceCalls;

namespace CallCentral.Calls
{
    public class SMSCall : BaseCall
    {
        public DateTime SendTime { get; private set; }
        public int NoOfCharacters { get; private set; }
        public string DestinationPhoneNumber { get; private set; }

        public SMSCall(string phoneNumber, DateTime sendTime, int lenght, string destinationPhoneNumber, string fromCountry, string toCountry)
            : base(phoneNumber, fromCountry, toCountry, ServiceCallType.SMS)
        {
            SendTime = sendTime;
            NoOfCharacters = lenght;
            DestinationPhoneNumber = destinationPhoneNumber;
        }

        public override DateTime GetStartTime()
        {
            return SendTime;
        }

        public override decimal GetUnitSize()
        {
            return NoOfCharacters;
        }

        protected override string GetDestination()
        {
            return DestinationPhoneNumber;
        }

        public override string ToString()
        {
            return string.Format("SMS sent to {0}, lenght {1} characters.", DestinationPhoneNumber, NoOfCharacters);
        }
    }
}

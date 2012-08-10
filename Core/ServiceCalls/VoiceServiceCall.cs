using System;

namespace Core.ServiceCalls
{
    public class VoiceServiceCall : BaseCall
    {
        public DateTime Start { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string DestinationPhoneNumber { get; private set; }

        public VoiceServiceCall(string phoneNumber, DateTime start, TimeSpan duration, string destinationPhoneNumber, string fromCountry, string toCountry)
            : base(phoneNumber, fromCountry, toCountry, ServiceCallType.Voice)
        {
            Start = start;
            Duration = duration;
            DestinationPhoneNumber = destinationPhoneNumber;
        }

        public override DateTime GetStartTime()
        {
            return Start;
        }

        public override decimal GetUnitSize()
        {
            return Convert.ToDecimal(Duration.TotalSeconds);
        }

        protected override string GetDestination()
        {
            return DestinationPhoneNumber;
        }

        public override string ToString()
        {
            return string.Format("VoiceCall to {0}, lasted {1}.", DestinationPhoneNumber, Duration);
        }
    }
}

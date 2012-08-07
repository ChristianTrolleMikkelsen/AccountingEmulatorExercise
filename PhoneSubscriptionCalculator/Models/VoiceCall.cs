using System;

namespace PhoneSubscriptionCalculator.Models
{
    public class VoiceCall : ICall
    {
        public DateTime Start { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string SourcePhoneNumber { get; private set; }
        public string DestinationPhoneNumber { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public VoiceCall(string sourcePhoneNumber, DateTime start, TimeSpan duration, string destinationPhoneNumber, string fromCountry, string toCountry)
        {
            Start = start;
            Duration = duration;
            SourcePhoneNumber = sourcePhoneNumber;
            DestinationPhoneNumber = destinationPhoneNumber;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }
    }
}

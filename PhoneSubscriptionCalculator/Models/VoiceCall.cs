using System;

namespace PhoneSubscriptionCalculator.Models
{
    public class VoiceCall : ICall
    {
        public DateTime Start { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public VoiceCall(DateTime start, TimeSpan duration, string phoneNumber, string fromCountry, string toCountry)
        {
            Start = start;
            Duration = duration;
            PhoneNumber = phoneNumber;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }
    }
}

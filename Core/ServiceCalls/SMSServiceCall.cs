﻿using System;

namespace Core.ServiceCalls
{
    public class SMSServiceCall : IServiceCall
    {
        public DateTime SendTime { get; private set; }
        public int NoOfCharacters { get; private set; }
        public string PhoneNumber { get; private set; }
        public string DestinationPhoneNumber { get; private set; }
        public string FromCountry { get; private set; }
        public string ToCountry { get; private set; }

        public SMSServiceCall(string sourcePhoneNumber, DateTime sendTime, int lenght, string destinationPhoneNumber, string fromCountry, string toCountry)
        {
            SendTime = sendTime;
            NoOfCharacters = lenght;
            PhoneNumber = sourcePhoneNumber;
            DestinationPhoneNumber = destinationPhoneNumber;
            FromCountry = fromCountry;
            ToCountry = toCountry;
        }

        public decimal GetUnitSize()
        {
            return NoOfCharacters;
        }
    }
}

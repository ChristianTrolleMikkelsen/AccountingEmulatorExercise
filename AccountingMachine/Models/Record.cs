using System;

namespace AccountingMachine.Models
{
    public class Record
    {
        public string PhoneNumber { get; private set; }
        public DateTime Registered { get; private set; }
        public string ChargeDescription { get; private set; }
        public string CallDescription { get; private set; }
        public decimal Charge { get; private set; }

        public Record(string phoneNumber, DateTime registered, string chargeDescription, string callDescription, decimal charge)
        {
            PhoneNumber = phoneNumber;
            Registered = registered;
            ChargeDescription = chargeDescription;
            CallDescription = callDescription;
            Charge = charge;
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-32} {2,-52} {3,10}", Registered, ChargeDescription, CallDescription, Charge);
        }
    }
}

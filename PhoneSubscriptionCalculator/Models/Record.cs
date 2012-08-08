namespace PhoneSubscriptionCalculator.Models
{
    public class Record
    {
        public string PhoneNumber { get; private set; }
        public string Description { get; private set; }
        public decimal Charge { get; private set; }

        public Record(string phoneNumber, string description, decimal charge)
        {
            PhoneNumber = phoneNumber;
            Description = description;
            Charge = charge;
        }
    }
}

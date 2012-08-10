namespace Core.Models
{
    public interface ISubscription
    {
        ICustomer Customer { get; }
        string PhoneNumber { get; }
        string LocalCountry { get; }
    }
}

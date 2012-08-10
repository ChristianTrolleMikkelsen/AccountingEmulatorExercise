namespace Core.Models
{
    public interface ICustomer
    {
        string Name { get; }
        CustomerStatus Status { get; }
    }
}

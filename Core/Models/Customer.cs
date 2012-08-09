namespace Core.Models
{
    public interface ICustomer
    {
        string Name { get; }
        CustomerStatus Status { get; }
    }

    public class Customer : ICustomer
    {
        public string Name { get; private set; }
        public CustomerStatus Status { get; private set; }

        public Customer(string name)
        {
            Name = name;
            Status = CustomerStatus.Normal;
        }

        public void SetCustomerStatus(CustomerStatus status)
        {
            Status = status;
        }
    }
}

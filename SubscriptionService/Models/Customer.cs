using Core.Models;

namespace SubscriptionService.Models
{
    internal class Customer : ICustomer
    {
        public string Name { get; private set; }
        public CustomerStatus Status { get; private set; }

        public Customer(string name, CustomerStatus status)
        {
            Name = name;
            Status = status;
        }
    }
}

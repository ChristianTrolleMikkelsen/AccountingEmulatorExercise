using Core.Models;
using SubscriptionServices.Models;

namespace SubscriptionServices
{
    public interface ICustomerRegistration
    {
        ICustomer CreateCustomer(string name, CustomerStatus status);
    }

    public class CustomerRegistration : ICustomerRegistration
    {
        public ICustomer CreateCustomer(string name, CustomerStatus status)
        {
            return new Customer(name, status);
        }
    }
}

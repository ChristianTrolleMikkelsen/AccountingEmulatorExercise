using System.Collections.Generic;
using System.Linq;

namespace PhoneSubscriptionCalculator.Models
{
    public interface ICustomer
    {
        string Name { get; }
        CustomerStatus Status { get; }

        void AddPhoneSubscription(IPhoneSubscription phoneSubscription);
        IPhoneSubscription GetPhoneSubscription(string phoneNumber);
    }

    public class Customer : ICustomer
    {
        public string Name { get; private set; }
        public CustomerStatus Status { get; private set; }

        private List<IPhoneSubscription> _phoneSubscriptions;

        public Customer(string name)
        {
            Name = name;
            Status = CustomerStatus.Normal;
            _phoneSubscriptions = new List<IPhoneSubscription>();
        }

        public void AddPhoneSubscription(IPhoneSubscription phoneSubscription)
        {
            _phoneSubscriptions.Add(phoneSubscription);
        }

        public IPhoneSubscription GetPhoneSubscription(string phoneNumber)
        {
            return _phoneSubscriptions.First(sub => sub.PhoneNumber == phoneNumber);
        }

        public void SetCustomerStatus(CustomerStatus status)
        {
            Status = status;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace PhoneSubscriptionCalculator.Models
{
    public interface ICustomer
    {
        string Name { get; }
        CustomerStatus Status { get; }

        void AddPhoneSubscription(ISubscription subscription);
        IEnumerable<ISubscription> GetPhoneSubscriptions();
    }

    public class Customer : ICustomer
    {
        public string Name { get; private set; }
        public CustomerStatus Status { get; private set; }

        private List<ISubscription> _phoneSubscriptions;

        public Customer(string name)
        {
            Name = name;
            Status = CustomerStatus.Normal;
            _phoneSubscriptions = new List<ISubscription>();
        }

        public void AddPhoneSubscription(ISubscription subscription)
        {
            _phoneSubscriptions.Add(subscription);
        }

        public IEnumerable<ISubscription> GetPhoneSubscriptions()
        {
            return _phoneSubscriptions;
        }

        public void SetCustomerStatus(CustomerStatus status)
        {
            Status = status;
        }
    }
}

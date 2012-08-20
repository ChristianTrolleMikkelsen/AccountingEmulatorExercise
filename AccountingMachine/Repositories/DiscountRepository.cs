using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;
using Core.Models;

namespace AccountingMachine.Repositories
{
    public interface IDiscountRepository
    {
        Discount GetDiscountForCustomerStatus(CustomerStatus customerStatus);
        void SaveDiscount(Discount record);
    }

    public class DiscountRepository : IDiscountRepository
    {
        private readonly List<Discount> _discounts;

        public DiscountRepository()
        {
            _discounts = new List<Discount>();
        }

        public Discount GetDiscountForCustomerStatus(CustomerStatus customerStatus)
        {
            return _discounts.First(record => record.CustomerStatus == customerStatus);
        }

        public void SaveDiscount(Discount record)
        {
            _discounts.Add(record);
        }
    }
}

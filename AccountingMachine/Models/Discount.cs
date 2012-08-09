using Core.Models;

namespace AccountingMachine.Models
{
    public class Discount
    {
        public CustomerStatus CustomerStatus { get; private set; }
        public decimal Percentage { get; private set; }

        public Discount(CustomerStatus customerStatus, decimal percentage)
        {
            CustomerStatus = customerStatus;
            Percentage = percentage;
        }

        public decimal GetResultingCostWithDiscount(decimal total)
        {
            return total * (Percentage == 0.0M ? 1 : (1 - Percentage));
        }
    }
}

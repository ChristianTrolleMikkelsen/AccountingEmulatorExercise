using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models;
using MoreLinq;

namespace AccountingMachine.Models
{
    public class Bill
    {
        public ISubscription Subscription { get; private set; }
        public IEnumerable<Record> Records { get; private set; }
        public Discount Discount { get; private set; }

        public Bill(ISubscription subscription, IEnumerable<Record> records, Discount discount)
        {
            Subscription = subscription;
            Records = records;
            Discount = discount;
        }

        public decimal GetSumOfRecords()
        {
            return Records.Sum(record => record.Charge);
        }

        public decimal GetTotal()
        {
            return Discount.GetResultingCostWithDiscount(GetSumOfRecords());
        }

        private string GenerateHeader()
        {
            return string.Format("Bill for {0} - Subscription {1}", Subscription.Customer.Name, Subscription.PhoneNumber);
        }

        private string GenerateContent()
        {
            var stringBuilder = new StringBuilder();

            Records.OrderByDescending(record => record.Registered)
                .ForEach(record => stringBuilder.AppendLine(record.ToString()));

            return stringBuilder.ToString();
        }

        private string GenerateTotals()
        {
            var total = GetSumOfRecords();
            var discountTotal = GetTotal();

            return string.Format("Sum: {0,41}\r\nDiscount due to status {1}: {2,10}%\r\n\r\nTotal: {3,40}", total,
                                                                                                Discount.CustomerStatus,
                                                                                                Discount.Percentage * 100,
                                                                                                discountTotal);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(GenerateHeader());
            stringBuilder.AppendLine("--------------------------------------------------------------------------");
            stringBuilder.AppendLine(GenerateContent());
            stringBuilder.AppendLine("--------------------------------------------------------------------------");
            stringBuilder.AppendLine(GenerateTotals());
            stringBuilder.AppendLine("--------------------------------------------------------------------------");

            return stringBuilder.ToString();
        }
    }
}

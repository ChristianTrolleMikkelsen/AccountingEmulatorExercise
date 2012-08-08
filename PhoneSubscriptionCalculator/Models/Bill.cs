using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneSubscriptionCalculator.Models
{
    public class Bill
    {
        private readonly IEnumerable<Record> _records;

        public Bill(IEnumerable<Record> records)
        {
            _records = records;
        }

        public decimal Total()
        {
            return _records.Sum(record => record.Charge);
        }
    }
}

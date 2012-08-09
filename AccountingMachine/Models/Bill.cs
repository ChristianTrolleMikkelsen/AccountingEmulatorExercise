using System.Collections.Generic;
using System.Linq;

namespace AccountingMachine.Models
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

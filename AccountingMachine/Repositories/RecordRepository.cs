using System.Collections.Generic;
using System.Linq;
using AccountingMachine.Models;

namespace AccountingMachine.Repositories
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetRecordsForPhoneNumber(string phoneNumber);
        void SaveRecord(Record record);
    }

    public class RecordRepository : IRecordRepository
    {
        private List<Record> _records;

        public RecordRepository()
        {
            _records = new List<Record>();
        }

        public IEnumerable<Record> GetRecordsForPhoneNumber(string phoneNumber)
        {
            return _records.Where(record => record.PhoneNumber == phoneNumber)
                            .ToList();
        }

        public void SaveRecord(Record record)
        {
            _records.Add(record);
        }
    }
}

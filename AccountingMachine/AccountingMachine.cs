using AccountingMachine.Generators;
using AccountingMachine.Models;

namespace AccountingMachine
{
    public interface IAccountingMachine
    {
        Bill GenerateBillForPhoneNumber(string phoneNumber);
    }

    public class AccountingMachine : IAccountingMachine
    {
        private readonly IRecordGenerator _recordGenerator;
        private readonly IBillGenerator _billGenerator;

        public AccountingMachine(IRecordGenerator recordGenerator, IBillGenerator billGenerator)
        {
            _recordGenerator = recordGenerator;
            _billGenerator = billGenerator;
        }

        public Bill GenerateBillForPhoneNumber(string phoneNumber)
        {
            _recordGenerator.GenerateAccountingRecordsForPhoneNumber(phoneNumber);

            return _billGenerator.GenerateBillForPhoneNumber(phoneNumber);
        }
    }
}

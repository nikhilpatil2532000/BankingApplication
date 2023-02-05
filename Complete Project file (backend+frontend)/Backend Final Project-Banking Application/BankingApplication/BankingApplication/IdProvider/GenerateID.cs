using BankingApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.IdProvider
{
    public class GenerateID : IGenerateID
    {
        private long transactionId;

        private static Random _random;
        private SmallOfficeContext _context;

        public GenerateID(SmallOfficeContext context)
        {
            _random = new Random();
            _context = context;
            transactionId = _context.BankTransactions.Select(x => x.TransactionId).OrderBy(x => x).LastOrDefault();
        }

        public long Generator16DigitNumber()
        {
            StringBuilder strBuild = new StringBuilder();
            while (strBuild.Length<16)
            {
                strBuild.Append(_random.Next(1000,9999).ToString());
            }
            return Convert.ToInt64(strBuild.ToString());
        }

        public long Generator16DigitUniqueNumber()
        {
            List<long> listOfUsedIDs = _context.BankAccounts.Select(x => x.AccountNo).ToList();
            long temp;
            do
            {
                temp = Generator16DigitNumber();
            } while (listOfUsedIDs.Contains(temp));
            return temp;
        }

        public long Generator4DigitNumber()
        {
            return Convert.ToInt64(_random.Next(1000, 9999));
        }

        public long Generator4DigitUniqueNumber()
        {
            List<long> listOfUsedIDs = _context.BankCustomers.Select(x => x.CustomerId).ToList();
            long temp;
            do
            {
                temp = Generator4DigitNumber();
            } while (listOfUsedIDs.Contains(temp));
            return temp;
        }

        public long GeneratorTransactionID()
        {
            return ++transactionId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.IdProvider
{
    public interface IGenerateID
    {
        long Generator16DigitNumber();
        long Generator16DigitUniqueNumber();
        long Generator4DigitNumber();
        long Generator4DigitUniqueNumber();
        long GeneratorTransactionID();
    }
}

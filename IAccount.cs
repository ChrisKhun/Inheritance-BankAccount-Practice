using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritanace_BankAccount
{
    internal interface IAccount
    {
        void Deposit(decimal amount);
        void Withdrawal(decimal amount);
        void Transfer(decimal amount);
    }
}

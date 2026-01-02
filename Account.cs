using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Inheritanace_BankAccount
{
    public class Account : IAccount
    {
        public string AccountType { get; set; }
        public int Id {  get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }


        public Account() { }


        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"{amount} deposited into {AccountType} account.\n Balance is now {Balance}");
        }
        public virtual void Withdrawal(decimal amount)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} withdrew into {AccountType} account.\n Balance is now {Balance}");
        }
        public virtual void Transfer(decimal amount)
        {
            Console.WriteLine("Money Transferred.");
        }
    }
}

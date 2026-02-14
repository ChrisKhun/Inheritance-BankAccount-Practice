using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Inheritanace_BankAccount.Data.Models
{
    public class Account : IAccount
    {
        public string AccountType { get; set; }
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public bool Minimum { get; set; }
        public int Pin { get; set; }
        

        public Account() { }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be more than 0.");
                return;
            }

            Balance += amount;
            Console.WriteLine($"{amount} deposited into {AccountType} account.\n Balance is now {Balance}");
        }

        public virtual void Withdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be more than 0.");
                return;
            }
            if (amount < amount)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"{amount} withdrew into {AccountType} account.\n Balance is now {Balance}");
        }

        public virtual void Transfer(decimal amount)
        {
            Console.WriteLine("Money Transferred.");
        }
    }
}

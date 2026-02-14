using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Data.Models;

namespace Inheritanace_BankAccount.Services
{
    public class SavingsAccountService : BaseService <SavingsAccount>
    {
        public SavingsAccountService(BankContext db) : base(db)
        {
        }

        public SavingsAccount? CreateAccount(string name, decimal deposit, int pin)
        {
            if (pin < 1000 || pin > 9999 || deposit < 0) { return null; }
            var account = new SavingsAccount()
            {
                Name = name,
                AccountType = "Savings",
                Balance = deposit,
                Pin = pin
            };
            
            account.Minimum = account.Balance >= 25; 
            
            return base.Add(account);
        }

        public SavingsAccount? Deposit(int accountId, decimal deposit, int pin)
        {          
            if (deposit < 1) {  return null; } // minimum deposit
            var account = base.GetById(accountId);

            if (account == null) { return null; }

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }

            account.Balance += deposit;
            return base.Update(account);
        }

        public SavingsAccount? Withdrawal(int accountId, decimal withdrawal, int pin)
        { // add logic where maybe like limit of 4 or so withdrwawls per month that are fee free? or something
            int withdrawalCount = 0; // this will change and i should write a statement to return null if count hits certain amount 
                                        // actually create a new datatype in database that counts the counts but needs to also track dates....
                                        // not sure yet...
            
            var account = base.GetById(accountId);

            if (account == null) { return null; }

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }
            if (withdrawal > account.Balance) { return null; }

            account.Balance -= withdrawal;
            return base.Update(account);
        }

        public bool DeleteAccount(int accountId, int pin)
        {
            var account = base.GetById(accountId);

            if (account == null) { return false; }

            Delete(account);

            return true;
        }
    }
}

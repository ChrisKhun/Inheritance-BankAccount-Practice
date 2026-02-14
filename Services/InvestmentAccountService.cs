using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Data.Models;

namespace Inheritanace_BankAccount.Services
{
    public class InvestmentAccountService : BaseService <InvestmentAccount>
    {
        public InvestmentAccountService(BankContext db) : base(db)
        {
        }

        public InvestmentAccount? CreateAccount(string name, decimal deposit, int pin)
        {
            if (pin < 1000 || pin > 9999 || deposit < 0) { return null; }
            var account = new InvestmentAccount()
            {
                Name = name,
                AccountType = "Investment",
                Balance = deposit,
                Pin = pin
            };

            account.Minimum = account.Balance >= 500;

            return base.Add(account);
        }

        public InvestmentAccount? Deposit(int accountId, decimal deposit, int pin)
        {
            if (deposit < 100) { return null; } // minimum deposit
            var account = base.GetById(accountId);

            if (account == null) { return null; }

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }

            account.Balance += deposit;
            return base.Update(account);
        }

        public InvestmentAccount? Withdrawal(int accountId, decimal withdrawal, int pin)
        { // add logic where maybe like limit of 1 or 2 or so withdrwawls per month that are fee free? or something
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Data.Models;
using Microsoft.Identity.Client;

namespace Inheritanace_BankAccount.Services
{
    public class CheckingAccountService : BaseService<CheckingAccount>
    {
        public CheckingAccountService(BankContext db) : base(db)
        {
        }

        public CheckingAccount? CreateAccount(string name, decimal deposit, int pin)
        {
            if (pin < 1000 || pin > 9999 || deposit < 0) { return null; }
            var account = new CheckingAccount()
            {
                Name = name,
                AccountType = "Checking",
                Balance = deposit,
                Pin = pin
            };
            // add logic where if user wants to avoid fees minimum of $500 deposit maybe like $3 a month or swomethig
            account.Minimum = account.Balance >= 500; // change if needed
            return base.Add(account);
        }

        public CheckingAccount? Deposit(int accountId, decimal deposit, int pin)
        {             // each account should have a pin along with account id for security? 
            if (deposit < 1) { return null; }  // minimum deposit
            var account = base.GetById(accountId);

            if (account == null) { return null; }

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }

            account.Balance += deposit;
            return base.Update(account);
        }

        public CheckingAccount? Withdrawal(int accountId, decimal withdrawal, int pin)
        {
            var account = base.GetById(accountId);

            if (account == null) { return null; }

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }
            if (withdrawal > account.Balance) { return null; }

            account.Balance -= withdrawal;
            return base.Update(account);
        }


        // for choice maybe do 1 checking, 2 savings, 3 investment
        public CheckingAccount? Transfer(int accountId, decimal transferAmount, string transferTo, int pin) // since function called _cas.transfer() other functions can have the same function without major changes
        {
            if (transferAmount < 1) { return null; }

            var account = base.GetById(accountId);

            if (account == null) { return null; };

            bool valid = IsPinValid(account, pin);

            if (!valid) { return null; }

            if (transferAmount > account.Balance) { return null; }

            account.Balance -= transferAmount;


            // for this have user put in something like account id use the abse function to get account
            // then find a way to find the account in another one like savings or something. 
            // the ids should be matching so checking, savings, investments should all have the same id
            // when user create an account they create three accounts but i could possible make it so
            // the user picks an account to open and just reserve the ids for that user if they
            // change their mind and opened the other ones to ensure those ids are reserved.
            throw new NotImplementedException();
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

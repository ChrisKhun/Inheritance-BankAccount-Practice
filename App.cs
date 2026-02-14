using System;
using System.Linq;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Data.Models;
using System.IO;
using System.Text.Json;
using Inheritanace_BankAccount.Services;
using Microsoft.Identity.Client;

namespace Inheritanace_BankAccount
{
    public class App
    {
        private readonly BankContext _db;
        private readonly CheckingAccountService _cas;
        private readonly SavingsAccountService _sas;
        private readonly InvestmentAccountService _ias;

        public App(BankContext db, CheckingAccountService cas, SavingsAccountService sas, InvestmentAccountService ias)
        {
            _db = db;
            this._cas = cas;
            this._sas = sas;
            this._ias = ias;
        }
        public void CreateAccount(string name, decimal deposit, int pin)
        {
            _cas.CreateAccount(name, deposit, pin); // deposit goes straight into checking
            _sas.CreateAccount(name, 0m, pin);
            _ias.CreateAccount(name, 0m, pin);
        }

        public void DeleteAccount(int accountId, int pin)
        {
            _cas.DeleteAccount(accountId, pin);
            _sas.DeleteAccount(accountId, pin);
            _ias.DeleteAccount(accountId, pin);
        }

        public CheckingAccount? ImportCheckingAccount(int accountId)
        {
            string fileName = $"checking_account_{accountId}.json";
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                fileName
            );

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return null;
            }

            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CheckingAccount? account =
                JsonSerializer.Deserialize<CheckingAccount>(json, options);

            return account;
        }

        public void ExportCheckingAccount(int accountId, int pin)
        {
            var account = _cas.GetById(accountId);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (account.Pin != pin)
            {
                Console.WriteLine("Invalid Pin.");
                return;
            }

            // serialize
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(account, options);

            // write to file
            string fileName = $"checking_account_{accountId}.json";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            fileName); // to desktop

            File.WriteAllText(path, json);

            Console.WriteLine($"Account exported to {path}");

        }

        public void Run()
        {
            // add logic here to interact with program.

            //_cas.CreateAccount("Lilly", 2394.23m, 2234);
            //_cas.Deposit(1004, 500m, 128);
            //_cas.Withdrawal(1004, 500m, 128);

            //CreateAccount("Brad", 1321.32m, 2912);

            //DeleteAccount(4, 2912);

            ExportCheckingAccount(5, 2234);

            Console.WriteLine("Checking Accounts:");

            foreach (var a in _db.CheckingAccounts.ToList())
            {
                Console.WriteLine($"{a.Id} | {a.AccountType} | {a.Name} | {a.Balance:C} | {a.Pin}");
            }
        }
    }
}

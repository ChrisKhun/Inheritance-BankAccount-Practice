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
        private readonly MondayServiceAPI _msa;

        public App(BankContext db, CheckingAccountService cas, SavingsAccountService sas, InvestmentAccountService ias, MondayServiceAPI msa)
        {
            _db = db;
            this._cas = cas;
            this._sas = sas;
            this._ias = ias;
            this._msa = msa;
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
                JsonSerializer.Deserialize<CheckingAccount>(json, options); // adds checking account from file to dataahase

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

        public async Task Run()
        {
            // add logic here to interact with program.

            //_cas.CreateAccount("Lilly", 2394.23m, 2234);
            //_cas.Deposit(1004, 500m, 128);
            //_cas.Withdrawal(1004, 500m, 128);

            //CreateAccount("Brad", 1321.32m, 2912);

            //DeleteAccount(1, 2912);


            await _msa.LogTransactionAsync("DEPOSIT", "Chris", 1, 13.23m);

            // INSTEAD OF calling logtransaction create a Deposit/withdrawal function in this app.cs have user
               // select what account they're depositing to so i can log and make transaction in one function call here.

            //await _msa.LogNewAccountAsync("Chris", 1000m);
            //await _msa.LogTransactionAsync("Chris", 500m, "Deposit", 1500m);
            //await _msa.LogDeletedAccountAsync("Chris");


            //ExportCheckingAccount(5, 2234);

            //Console.WriteLine("Checking Accounts:");

            foreach (var a in _db.CheckingAccounts.ToList())
            {
                Console.WriteLine($"{a.Id} | {a.AccountType} | {a.Name} | {a.Balance:C} | {a.Pin}");
            }
        }
    }
}

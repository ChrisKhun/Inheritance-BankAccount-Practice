using Inheritanace_BankAccount;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Inheritanace_BankAccount.Data;

var builder = Host.CreateApplicationBuilder(args);

// Add services (like DbContext)
builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Test_Bank;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddTransient<App>();

var app = builder.Build();
app.Services.GetRequiredService<App>();
// Run your app

// use bankcontext



var bankAccounts = new List<Account>();

var checkingAccount = new CheckingAccount();
checkingAccount.AccountType = "checking";
checkingAccount.Id = 123;
checkingAccount.Name = "Greg";
checkingAccount.Balance = 250.34m;

var savingsAccount = new SavingsAccount();
savingsAccount.AccountType = "savings";
savingsAccount.Id = 321;
savingsAccount.Name = "Bob";
savingsAccount.Balance = 1583.34m;

var investmentAccount = new InvestmentAccount();
investmentAccount.AccountType = "investment";
investmentAccount.Id = 213;
investmentAccount.Name = "Anne";
investmentAccount.Balance = 2500.34m;

bankAccounts.Add(checkingAccount);
bankAccounts.Add(savingsAccount);
bankAccounts.Add(investmentAccount);


checkingAccount.Deposit(500);
savingsAccount.Deposit(900);

void GetBalance(Account acct)
{
    Console.WriteLine(acct.Balance);
}

bool DepositMoney(Account acct, decimal dollars)
{
    acct.Deposit(dollars);
    return true;
}

GetBalance(checkingAccount);
GetBalance(savingsAccount);

DepositMoney(savingsAccount, 1100);
DepositMoney(checkingAccount, 500);





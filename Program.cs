using Inheritanace_BankAccount;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=AccountsDB;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddTransient<App>();

builder.Services.AddScoped<CheckingAccountService>();
builder.Services.AddScoped<SavingsAccountService>();
builder.Services.AddScoped<InvestmentAccountService>();


using var host = builder.Build();

var app = host.Services.GetRequiredService<App>();

app.Run();








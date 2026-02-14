using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Inheritanace_BankAccount.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
        : base(options)
        {
        }
        
 
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<InvestmentAccount> InvestmentAccounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPT inheritance

            modelBuilder.Entity<CheckingAccount>().ToTable("CheckingAccounts");
            modelBuilder.Entity<SavingsAccount>().ToTable("SavingsAccounts");
            modelBuilder.Entity<InvestmentAccount>().ToTable("InvestmentAccounts");
        }
    }
}

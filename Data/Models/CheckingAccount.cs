using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inheritanace_BankAccount.Data.Models
{
    //[Table(name: "CheckingAccounts")]
    public class CheckingAccount : Account
    {
        public override int Id { get; set; }
        public CheckingAccount() { }

        public override void Withdrawal(decimal amount)
        {
            // withdrawl fee
            const decimal fee = 1.00m;

            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be greater than zero.");
                return;
            }

            var total = amount + fee;
            if (Balance < total)
            {
                Console.WriteLine($"Insufficient funds (need {total:C} including a {fee:C} fee).");
                return;
            }

            Balance -= total;
        }

    }
}
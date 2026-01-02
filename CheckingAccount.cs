using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritanace_BankAccount
{
    [Table(name: "CheckingAccount")]
    public class CheckingAccount : Account
    {
        public CheckingAccount() { }

        public override void Withdrawal(decimal amount)
        {
            Console.WriteLine("take money out.");
        }
    }
}

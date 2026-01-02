using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data;

namespace Inheritanace_BankAccount
{
    public class App
    {
        private readonly BankContext _db;
        public App(BankContext bankContext)
        {
            _db = bankContext;

            _db.CheckingAccounts.Add(new CheckingAccount { });

            var accts = _db.CheckingAccounts.Where(x => x.Balance > 0).ToList();
        }
    }
}

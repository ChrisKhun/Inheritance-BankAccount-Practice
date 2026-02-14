using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Inheritanace_BankAccount.Data;
using Inheritanace_BankAccount.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Inheritanace_BankAccount.Services
{
    public class BaseService<TModel> where TModel : class
    {
        BankContext _db;

        public void Run() { }

        public BaseService(BankContext db)
        {
            _db = db;
        }

        // CREATE
        public virtual TModel Add(TModel account)
        {
            _db.Set<TModel>().Add(account);
            _db.SaveChanges();
            return account;
        }

        // DELETE
        public virtual bool Delete(TModel account)
        {
            _db.Set<TModel>().Remove(account);
            _db.SaveChanges();
            return true;
        }

        // READ
        public virtual TModel? GetById(int accountId)
        {
            return _db.Set<TModel>()
              .FirstOrDefault(x => EF.Property<int>(x, "Id") == accountId);
        }

        // READ
        public virtual bool IsPinValid(TModel account, int pin)
        {
            return account != null && ((Account)(object)account).Pin == pin;
        }

        // READ
        public virtual List<TModel> GetAll()
        {
            var accounts = _db.Set<TModel>().ToList();
            return accounts;
        }

        // UPDATE
        public virtual TModel? Update(TModel account)
        {
            _db.Set<TModel>().Update(account);
            _db.SaveChanges();
            return null;
        }

        // find any query 
        // ex. var big = _cas.Find(a => a.Balance > 1000);
        //     var named = _cas.Find(a => a.Name.Contains("Chris"));

        public virtual List<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            return _db.Set<TModel>()
                      .Where(predicate)
                      .ToList();
        }

    }
}

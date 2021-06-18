using DataLayer.Entities;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Model.Misc;

namespace DataLayer
{
   public class AccountService: IAccountService
    {
        private readonly ApplicationContext _db = new ApplicationContext();
        private readonly AccounTransactionHistoryService _accounTransactionHistoryService = new AccounTransactionHistoryService(); 
       
        public Account GetAccount(int memberId)
        {
            return _db.Accounts.FirstOrDefault(x => x.MemberId == memberId);
        }

        public Account AddAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
          
            return account;
        }

        public bool DeleteAccount(int accountId)
        {
            var acc = _db.Accounts.SingleOrDefault(x => x.AccountId == accountId);
            _db.Accounts.Remove(acc);
            _db.SaveChanges();
            return true;
        }

        public Account AddMoney(int memberId, decimal amount, TransactionStatus transactionStatus)
        {
            var acc = _db.Accounts.SingleOrDefault(x => x.MemberId == memberId);
            acc.Amount += amount;
            _db.SaveChanges();
            _accounTransactionHistoryService.AddTransactionHistory(new AccountTransactionHistory
            {
                AccountId = acc.AccountId,
                Amount = amount,
                Status = transactionStatus.ToString(),
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }) ;
            return acc;
        }

        public Account DeductMoney( int memberId, decimal amount, TransactionStatus transactionStatus)
        {
            var acc = _db.Accounts.SingleOrDefault(x => x.MemberId == memberId);
            acc.Amount -= amount;
            _db.SaveChanges();
            _accounTransactionHistoryService.AddTransactionHistory(new AccountTransactionHistory
            {
                AccountId = acc.AccountId,
                Amount = amount,
                Status = transactionStatus.ToString(),
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });
            return acc;
        }

    }
}

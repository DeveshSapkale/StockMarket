using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Model.Misc;

namespace DataLayer
{
   public class AccountService
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

        public bool AddMoney(int memberId, decimal amount)
        {
            var acc = _db.Accounts.SingleOrDefault(x => x.AccountId == accountId && x.MemberId == memberId);
            acc.Amount += amount;
            _accounTransactionHistoryService.AddTransactionHistory(new AccountTransactionHistory
            {
                AccountId = accountId,
                Amount = amount,
                Status = TransactionStatus.MONEY_DEPOSITED.ToString(),
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }) ;
            _db.SaveChanges();

            return true;
        }

        public bool DeductMoney( int memberId, decimal amount)
        {
            var acc = _db.Accounts.SingleOrDefault(x => x.AccountId == accountId && x.MemberId == memberId);
            acc.Amount -= amount;
            _db.SaveChanges();
            return true;
        }

    }
}

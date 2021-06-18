using DataLayer.Entities;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AccounTransactionHistoryService: IAccountTransactionHistoryService
    {
        private readonly ApplicationContext _db = new ApplicationContext();

        public IEnumerable<AccountTransactionHistory> GetTransaction(int accountId)
        {
          return  _db.AccountTransactionHistories.Where(x => x.AccountId == accountId);
        }

        public AccountTransactionHistory AddTransactionHistory(AccountTransactionHistory accountTransactionHistory)
        {
            _db.AccountTransactionHistories.Add(accountTransactionHistory);
            _db.SaveChanges();
            return accountTransactionHistory;
        }
    }
}

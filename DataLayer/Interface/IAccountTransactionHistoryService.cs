using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
   public interface IAccountTransactionHistoryService
    {
        IEnumerable<AccountTransactionHistory> GetTransaction(int accountId);
        AccountTransactionHistory AddTransactionHistory(AccountTransactionHistory accountTransactionHistory);
    }
}

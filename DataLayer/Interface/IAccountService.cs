using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Model.Misc;

namespace DataLayer.Interface
{
    public interface IAccountService
    {
        Account GetAccount(int memberId);
        Account AddAccount(Account account);
        bool DeleteAccount(int accountId);
        Account AddMoney(int memberId, decimal amount, TransactionStatus transactionStatus);
        Account DeductMoney(int memberId, decimal amount, TransactionStatus transactionStatus);

    }
}

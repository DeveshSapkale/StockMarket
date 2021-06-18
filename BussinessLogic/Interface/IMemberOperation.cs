using DataLayer.Entities;
using System.Collections.Generic;

namespace BussinessLogic.Interface
{
    public interface IMemberOperation
    {
        Member Login(string userName, string password);
        Member Register(Member member);
        IEnumerable<MemberHolding> MemberHoldings(int memberId);
        IEnumerable<Stock> GetSharesByName(string stockName);
        Account AddMoney(int memberId, decimal amount);
        Account WithdrawMoney(int memberId, decimal amount);
        IEnumerable<AccountTransactionHistory> GetTransaction(int accountId);
        IEnumerable<Order> GetOrders(int memberId);
        Order GetOrder(int orderId);
        Account GetAccount(int memberId);
    }
}

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
    }
}

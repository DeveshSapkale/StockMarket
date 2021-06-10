using DataLayer.Entities;

namespace StockMarket.Interface
{
    public interface ILoginService
    {
        Member Login(string userName, string secret);
        Member Register(Member member);
    }
}

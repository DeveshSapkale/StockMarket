using DataLayer;
using DataLayer.Entities;
using StockMarket.Interface;
using System;
using System.Linq;

namespace BussinessLogic
{
    public class LoginService : ILoginService
    {
        public ApplicationContext _db = new ApplicationContext();

        public Member Login(string userName, string secret)
        {
            return _db.Members.SingleOrDefault(mem => mem.UserName == userName && mem.Password == secret);
        }

        public Member Register(Member member)
        {
            _db.Members.Add(member);
            _db.SaveChanges();

            _db.Accounts.Add(new Account { MemberId = member.Id, Amount = 0, CreationDate = DateTime.Now, ModifiedDate = DateTime.Now });
            _db.SaveChanges();
            return member;
        }

        public bool ForgetPassword(string userName, string newPassword)
        {
            var member = _db.Members.SingleOrDefault(mem => mem.UserName == userName);
            member.Password = newPassword;
            _db.SaveChanges();

            return true;
        }
    }
}

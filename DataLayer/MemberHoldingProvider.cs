using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MemberHoldingProvider
    {
        private static readonly ApplicationContext _db = new ApplicationContext();
        public MemberHoldingProvider()
        {

        }

        public static IEnumerable<MemberHolding> GetMemberHoldings(int memberId)
        {
            return _db.MemberHoldings.Where(mem => mem.MemberId == memberId).ToList();
        }
    }
}

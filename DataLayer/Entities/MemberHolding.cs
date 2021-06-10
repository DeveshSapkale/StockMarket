using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [DataContract]
    public class MemberHolding
    {
        [DataMember]
        public int MemberHoldingId { get; set; }
        [DataMember]
        public int MemberId { get; set; }
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal InvestedAmount { get; set; }
    }
}

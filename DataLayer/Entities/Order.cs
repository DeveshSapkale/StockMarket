using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public int MemberId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int FulfilledQuatity { get; set; }
        [DataMember]
        public string OrderType { get; set; }
        [DataMember]
        public string OrderValidity { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime PurchaseTime { get; set; }
    }
}

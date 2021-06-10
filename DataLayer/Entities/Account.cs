using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public int MemberId { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public int MyProperty { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }
}

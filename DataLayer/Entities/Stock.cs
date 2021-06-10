using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [DataContract]
    public class Stock
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Exchange { get; set; } = "NYSE";
        
        [DataMember]
        public string Symbol { get; set; }
        
        [DataMember]
        public string CompanyName { get; set; }
        
        [DataMember] 
        public int Volume { get; set; }
        [DataMember]
        public decimal InitialPrice { get; set; }
        
        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public decimal LivePrice { get; set; } = 0;

    }
}

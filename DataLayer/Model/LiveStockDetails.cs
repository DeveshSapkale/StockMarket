using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [NotMapped]
    [DataContract]
    public class LiveStockDetails: Stock
    {
        [DataMember]
        public decimal LivePrice { get; set; } = 0;

        [DataMember]
        public decimal TodaysLow { get; set; }

        [DataMember]
        public decimal TodaysHigh { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class StockTradeBid
    {
        public int OrderId { get; set; }
        public int StockId { get; set; }
        public int MemberId { get; set; }
        public int BidQuantity { get; set; }
        public decimal BidUnitPrice { get; set; }
    }
}

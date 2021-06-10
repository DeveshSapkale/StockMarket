using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class StockTradeAsk
    {
        public int OrderId { get; set; }
        public int StockId { get; set; }
        public int AskQuantity { get; set; }
        public decimal AskUnitPrice { get; set; }
    }
}

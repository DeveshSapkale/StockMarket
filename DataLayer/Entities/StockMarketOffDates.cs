using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class StockMarketOffDates
    {
        public int StockMarketOffDatesId { get; set; }
        public DateTime OffDate { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

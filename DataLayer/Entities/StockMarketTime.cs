using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class StockMarketTime
    {
        public int StockMarketTimeId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

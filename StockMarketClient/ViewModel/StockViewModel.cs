using StockMarketClient.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketClient.ViewModel
{
    public class StockViewModel: ObservableObject
    {
        public int Id { get; set; }
        
        public string Exchange { get; set; } = "NYSE";
      
        public string Symbol { get; set; }
        
        public string CompanyName { get; set; }

        public Int64 Volume { get; set; }
        public decimal InitialPrice { get; set; }

        public DateTime CreationDate { get; set; }

        private decimal _livePrice;
        
        public decimal LivePrice
        {
            get
            {
                return _livePrice;
            }
            set
            {
                _livePrice = value;
                OnPropertyChanged("LivePrice");
            }
        }
    }
}

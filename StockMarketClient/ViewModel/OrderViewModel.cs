using StockMarketClient.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketClient.ViewModel
{
    public class OrderViewModel : ObservableObject
    {
        public int OrderId { get; set; }
        public int StockId { get; set; }
        public int MemberId { get; set; }
        public int Quantity { get; set; }
        public int FulfilledQuatity { get; set; }
        public string OrderType { get; set; }
        public string OrderValidity { get; set; }
        public decimal UnitPrice { get; set; }
        private string _liveStatus;
        public string Status
        {
            get
            {
                return _liveStatus;
            }
            set
            {
                _liveStatus = value;
                OnPropertyChanged("Status");
            }
        }
        public DateTime PurchaseTime { get; set; }
    }
}

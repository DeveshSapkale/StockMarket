using DataLayer.Entities;
using StockMarketClient.Misc;
using StockMarketClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace StockMarketClient
{
    public static class SimplePooling
    {
        public static ObservableCollection<StockViewModel> LiveStocks = new ObservableCollection<StockViewModel>();
        public static ObservableCollection<OrderViewModel> LiveOrders = new ObservableCollection<OrderViewModel>();
        private static readonly LiveStockData.LiveStockDataSvcClient serviceClient = new LiveStockData.LiveStockDataSvcClient();
        private static readonly TimeSpan _fluctuationTimerInterval = TimeSpan.FromSeconds(20);
        private static Timer _timer;
        public static bool IsRunning = false;

        public static void Start()
        {
            Poll();
            _timer = new Timer(_fluctuationTimerInterval.TotalMilliseconds);
            _timer.Elapsed += (sender, args) => Poll();
            _timer.Start();
            IsRunning = true;

        }

        public static void Stop()
        {
            _timer.Stop();
            _timer = null;
            IsRunning = false;
        }

        private static void Poll()
        {
           
                try
                {
                    var result =  serviceClient.GetLiveStockDetails();
                    UpdateLocalEntries(result);

                }
                catch (Exception caught)
                {
                }
        }

        private static void UpdateLocalEntries(IEnumerable<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                var item = LiveStocks.SingleOrDefault(x => x.Id == stock.Id);
                if (item == null)
                    AddMissingStock(stock);
                else
                    item.LivePrice = stock.LivePrice;
            }
        }

        private static void AddMissingStock(Stock stock)
        {
            LiveStocks.Add(MappingsConfiguration.mapper.Map<StockViewModel>(stock));
        }
    }
}

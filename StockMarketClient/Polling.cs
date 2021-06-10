using AutoMapper;
using DataLayer.Entities;
using StockMarketClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarketClient
{
    partial class Polling : ServiceBase
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task mainTask = null;
        public static ObservableCollection<StockViewModel> LiveStocks = new ObservableCollection<StockViewModel>();
        LiveStockData.LiveStockDataSvcClient serviceClient;
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Stock, StockViewModel>());

        public Polling()
        {
            InitializeComponent();
            serviceClient = new LiveStockData.LiveStockDataSvcClient();
        }

        protected override void OnStart(string[] args)
        {
            mainTask = new Task(Poll, cts.Token, TaskCreationOptions.LongRunning);
            mainTask.Start();
            
        }

        protected override void OnStop()
        {
            cts.Cancel();
            mainTask.Wait();
        }

        private async void Poll()
        {
            CancellationToken cancellation = cts.Token;
            TimeSpan interval = TimeSpan.FromSeconds(1);
            while (!cancellation.WaitHandle.WaitOne(interval))
            {
                try
                {
                    var result = await serviceClient.GetLiveStockDetailsAsync();
                    UpdateLocalEntries(result);

                    // Occasionally check the cancellation state.
                    if (cancellation.IsCancellationRequested)
                    {
                        break;
                    }
                }
                catch (Exception caught)
                {
                }
            }
        }

        private static void UpdateLocalEntries(IEnumerable<Stock> stocks)
        {
            foreach( var stock in LiveStocks)
            {
                stock.LivePrice = stocks.First(x => x.Id == stock.Id).LivePrice;
            }
        }
    }
}

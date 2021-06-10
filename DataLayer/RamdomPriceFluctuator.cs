using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DataLayer
{
    public static class RamdomPriceFluctuator
    {
        public static bool IsRunning = false; 
        private static Random _random = new Random();
        private static readonly TimeSpan _fluctuationTimerInterval = TimeSpan.FromSeconds(10);
        private static Timer _timer;
        public static int Fluctuate()
        {
           return _random.Next(-10, 10);
        }

        public static void Start()
        {
            _timer = new Timer(_fluctuationTimerInterval.TotalMilliseconds);
            _timer.Elapsed += (sender, args) => LiveStockDetailsProvider.FluctuateStockPrice(Fluctuate());
            _timer.Start();
            IsRunning = true;
        }

        public static void Stop()
        {
            _timer.Stop();
            _timer = null;
            IsRunning = false;
        }
    }
}

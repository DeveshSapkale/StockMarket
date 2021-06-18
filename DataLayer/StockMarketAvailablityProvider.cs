using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class StockMarketAvailablityProvider
    {
        private static readonly ApplicationContext _db = new ApplicationContext();
        public static TimeSpan StockMarketStartTime { get; set; }
        public static TimeSpan StockMarketStopTime { get; set; }

        public StockMarketAvailablityProvider()
        {
            IsvalidTime();
        }

        public static bool IsMarketAvailable()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                return false;
            if (_db.StockMarketOffDates.Any(x => DbFunctions.TruncateTime(x.OffDate) == DbFunctions.TruncateTime(DateTime.Today)))
                return false;
            if (!IsvalidTime())
                return false;
            return true;
        }

        private static bool IsvalidTime()
        {
            var stockMarketTime = _db.StockMarketTimes.FirstOrDefault();
            var stockMarketStartTime = stockMarketTime.StartTime.Split(':');
            var stockMarketStopTime = stockMarketTime.StopTime.Split(':');
            
            var stockMarketStartHour = Convert.ToInt32(stockMarketStartTime[0]);
            var stockMarketStartMinute = Convert.ToInt32(stockMarketStartTime[1]);
            var stockMarketStartSecond = Convert.ToInt32(stockMarketStartTime[2]);

            var stockMarketStopHour = Convert.ToInt32(stockMarketStopTime[0]);
            var stockMarketStopMinute = Convert.ToInt32(stockMarketStopTime[1]);
            var stockMarketStopSecond = Convert.ToInt32(stockMarketStopTime[2]);

            var currentTime = DateTime.Now.TimeOfDay;
            StockMarketStartTime = new TimeSpan(stockMarketStartHour, stockMarketStartMinute, stockMarketStartSecond);
            StockMarketStopTime = new TimeSpan(stockMarketStopHour, stockMarketStopMinute, stockMarketStopSecond);

            if ((TimeSpan.Compare(currentTime, StockMarketStartTime) == -1) || (TimeSpan.Compare(currentTime, StockMarketStopTime) == 1))
                return false;

            return true;
        }
    }
}

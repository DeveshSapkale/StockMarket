using BussinessLogic.Interface;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class MarketDateOffService : IMarketDateOffService
    {
        ApplicationContext _db = new ApplicationContext();

        public StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates)
        {
            _db.StockMarketOffDates.Add(stockMarketOffDates);
            _db.SaveChanges();
            return stockMarketOffDates;
        }

        public bool Delete(int stockMarketOffDateId)
        {
            var item = _db.StockMarketOffDates.First(x => x.StockMarketOffDatesId == stockMarketOffDateId);
            _db.StockMarketOffDates.Remove(item);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<StockMarketOffDates> GetStockMarketOffDates()
        {
            return _db.StockMarketOffDates;
        }
    }
}

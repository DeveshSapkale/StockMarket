using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interface
{
    public interface IMarketDateOffService
    {
        StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates);
        bool Delete(int stockMarketOffDateId);
        IEnumerable<StockMarketOffDates> GetStockMarketOffDates();
    }

}

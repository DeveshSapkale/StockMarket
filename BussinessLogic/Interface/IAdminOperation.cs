using DataLayer.Entities;
using System.Collections.Generic;

namespace BussinessLogic.Interface
{
    public interface IAdminOperation
    {
        Stock CreateStock(Stock stock);
        Stock UpdateStock(Stock stock);
        List<Stock> GetStocks();
        Stock GetStock(int stockId);
        bool DeleteStock(int stockId);
        StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates);
        bool Delete(int stockMarketOffDateId);
        IEnumerable<StockMarketOffDates> GetStockMarketOffDates();
    }
}

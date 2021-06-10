using BussinessLogic.Interface;
using DataLayer.Entities;
using System.Collections.Generic;

namespace BussinessLogic
{
    public class AdminOperation: IAdminOperation
    {
        private readonly IStockService _stockService;
        private readonly IMarketDateOffService _marketDateOffService;
        public AdminOperation(IStockService stockService, IMarketDateOffService marketDateOffService)
        {
            _stockService = stockService;
            _marketDateOffService = marketDateOffService;
        }
        
        public Stock CreateStock(Stock stock)
        {
            return _stockService.CreateStock(stock);
        }

        public Stock UpdateStock(Stock stock)
        {
            return _stockService.UpdateStock(stock);
        }

        public List<Stock> GetStocks()
        {
            return _stockService.GetStocks();
        }

        public Stock GetStock(int stockId)
        {
            return _stockService.GetStock(stockId);
        }

        public bool DeleteStock(int stockId)
        {
            return _stockService.DeleteStock(stockId);
        }

        public StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates)
        {
            return _marketDateOffService.Add(stockMarketOffDates);
        }

        public bool Delete(int stockMarketOffDateId)
        {
            return _marketDateOffService.Delete(stockMarketOffDateId);
        }

        public IEnumerable<StockMarketOffDates> GetStockMarketOffDates()
        {
            return _marketDateOffService.GetStockMarketOffDates();
        }
    }
}

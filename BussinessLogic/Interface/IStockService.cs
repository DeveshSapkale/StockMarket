using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interface
{
    public interface IStockService
    {
        Stock CreateStock(Stock stock);
        Stock UpdateStock(Stock stock);
        List<Stock> GetStocks();
        Stock GetStock(int id);
        bool DeleteStock(int stockId);
        List<Stock> GetStocksByName(string stockName);
    }
}

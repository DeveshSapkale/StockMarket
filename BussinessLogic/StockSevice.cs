using AutoMapper;
using BussinessLogic.Interface;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BussinessLogic
{
    public class StockSevice : IStockService
    {
        private ApplicationContext _db = new ApplicationContext();

        public Stock GetStock(int stockId)
        {
            try
            {
                var stock = _db.Stocks.Single(stk => stk.Id == stockId);
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception occured:{ex}");
            }
        }

        public List<Stock> GetStocks()
        {
            try
            {
                var stock = _db.Stocks.ToList();
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception occured:{ex}");
            }
        }

        public List<Stock> GetStocksByName(string stockName)
        {
            return _db.Stocks.Where(x => x.CompanyName.Contains(stockName)).ToList();
        }

        public Stock CreateStock(Stock stock)
        {
            if (stock == null)
                throw new Exception($"Value Cannot be null: stock  in {MethodBase.GetCurrentMethod().Name}");
            _db.Stocks.Add(stock);
            _db.SaveChanges();
            return stock;
        }

        public Stock UpdateStock(Stock stock)
        {
            try
            {
                var retStock = _db.Stocks.Single(stk => stk.Id == stock.Id);
                retStock.Symbol = stock.Symbol;
                retStock.Volume = stock.Volume;
                retStock.InitialPrice = stock.InitialPrice;
                retStock.Exchange = stock.Exchange;
                retStock.CompanyName = stock.CompanyName;

                _db.SaveChanges();
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception occured:{ex}");
            }
        }

        public bool DeleteStock(int stockId)
        {
            try
            {
                var stock = _db.Stocks.Single(stk => stk.Id == stockId);
                _db.Stocks.Remove(stock);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Exception occured:{ex}");
            }
        }
    }
}

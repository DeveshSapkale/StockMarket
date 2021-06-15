
using BussinessLogic;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace StockMarket
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class StockMarketSvc : IStockMarket
    {
        private readonly AdminOperation _adminOperation = new AdminOperation(new StockSevice(), new MarketDateOffService());
        private readonly MemberOperation _memberOperation = new MemberOperation(new LoginService(), new OrderService(), new StockSevice());
        private readonly StockMarketAvailablityProvider _stockMarketAvailablityProvider = new StockMarketAvailablityProvider();
        #region Admin Operations
        public Stock CreateStock(Stock stock)
        {
            return _adminOperation.CreateStock(stock);
        }

        public bool DeleteStock(int stockId)
        {
            return _adminOperation.DeleteStock(stockId);
        }

        public Stock GetStock(int stockId)
        {
            return _adminOperation.GetStock(stockId);
        }

        public List<Stock> GetStocks()
        {
            return _adminOperation.GetStocks();
        }

        public Stock UpdateStock(Stock stock)
        {
            return _adminOperation.UpdateStock(stock);
        }
        #endregion Admin Operations

        #region Authentication Operation
        public Member Login(string userName, string password)
        {
            return _memberOperation.Login(userName, password);
        }

        public Member Register(Member member)
        {
            return _memberOperation.Register(member);
        }
        #endregion Authentication Operation

        #region Member Operations
        public IEnumerable<MemberHolding> GetMemberHoldings(int memberId)
        {
            return _memberOperation.MemberHoldings(memberId);
        }

        public Order SellShare(Order order)
        {
            if (!StockMarketAvailablityProvider.IsMarketAvailable())
                throw new Exception("Martket is not available at those hours of the day");

            return null;
        }

        public Order BuyShare(Order order)
        {
            if (!StockMarketAvailablityProvider.IsMarketAvailable())
                throw new Exception("Martket is not available at those hours of the day");

            return null;
        }

        public StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates)
        {
            return _adminOperation.Add(stockMarketOffDates);
        }

        public bool Delete(int stockMarketOffDateId)
        {
            return _adminOperation.Delete(stockMarketOffDateId);
        }

        public IEnumerable<StockMarketOffDates> GetStockMarketOffDates()
        {
            return _adminOperation.GetStockMarketOffDates();
        }

        public IEnumerable<Stock> GetSharesByName(string stockName)
        {
            return _memberOperation.GetSharesByName(stockName);
        }

        #endregion Member Operations


    }
}
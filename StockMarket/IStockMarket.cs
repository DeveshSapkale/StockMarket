using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StockMarket
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract()]
    public interface IStockMarket
    {
        #region Authentication/Registration
        [OperationContract]
        Member Login(string userName, string password);
        [OperationContract]
        Member Register(Member member);
        #endregion

        #region Admin Functionalities
        [OperationContract]
        Stock CreateStock(Stock stock);
        [OperationContract]
        Stock UpdateStock(Stock stock);
        [OperationContract]
        List<Stock> GetStocks();
        [OperationContract]
        Stock GetStock(int id);
        [OperationContract]
        bool DeleteStock(int stockId);
        [OperationContract]
        StockMarketOffDates Add(StockMarketOffDates stockMarketOffDates);
        [OperationContract]
        bool Delete(int stockMarketOffDateId);
        [OperationContract]
        IEnumerable<StockMarketOffDates> GetStockMarketOffDates();

        #endregion

        #region MemberOperations
        [OperationContract]
        IEnumerable<MemberHolding> GetMemberHoldings(int memberId);
        [OperationContract]
        Order SellShare(Order order);
        [OperationContract]
        Order BuyShare(Order order);
        #endregion Memberoperations

    }

}

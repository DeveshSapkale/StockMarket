using BussinessLogic.Interface;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Interface;
using DataLayer.Model;
using StockMarket.Interface;
using System.Collections.Generic;
using static DataLayer.Model.Misc;

namespace BussinessLogic
{
    public class MemberOperation: IMemberOperation
    {
        private readonly ILoginService _loginService;
        private readonly IOrderService _orderService;
        private readonly IStockService _stockService;

        public MemberOperation(ILoginService loginService, IOrderService orderService, IStockService stockService)
        {
            _loginService = loginService;
            _orderService = orderService;
            _stockService = stockService;
        }

        public Member Login(string userName, string password)
        {
            return _loginService.Login(userName, password);
        }

        public IEnumerable<MemberHolding> MemberHoldings(int memberId)
        {
            return MemberHoldingProvider.GetMemberHoldings(memberId);
        }

        public Member Register(Member member)
        {
           return _loginService.Register(member);
        }

        public Order BuyShare(Order order)
        {
            order.Status = OrderStatus.PENDING.ToString();
            var ord =  _orderService.PlaceOrder(order);
            LiveStockDetailsProvider.AddShareBid(new StockTradeBid() { OrderId = ord.OrderId, MemberId = ord.MemberId, StockId = ord.StockId, BidQuantity = ord.Quantity, BidUnitPrice = ord.UnitPrice });
            LiveStockDetailsProvider.LiveOrders.Add(order);
            if(order.OrderType == OrderValidity.IMMEDIATE.ToString())
            {
                LiveStockDetailsProvider.CheckIfOrderCanBeResolved(order);
            }

            return null;

        }

        public Order SellShare(Order ord)
        {
            ord.Status = OrderStatus.PENDING.ToString();
            _orderService.PlaceOrder(ord);
            LiveStockDetailsProvider.AddShareAsk(new StockTradeAsk() { OrderId = ord.OrderId, MemberId = ord.MemberId, StockId = ord.StockId, AskQuantity = ord.Quantity, AskUnitPrice = ord.UnitPrice });
            LiveStockDetailsProvider.LiveOrders.Add(ord);
            return null;
        }

        public IEnumerable<Stock> GetSharesByName(string stockName)
        {
            return _stockService.GetStocksByName(stockName);
        }
    }
}

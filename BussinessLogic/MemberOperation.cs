using BussinessLogic.Interface;
using DataLayer;
using DataLayer.Entities;
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
        public MemberOperation(ILoginService loginService, IOrderService orderService)
        {
            _loginService = loginService;
            _orderService = orderService;
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
            LiveStockDetailsProvider.AddShareBid(new StockTradeBid() { OrderId = ord.OrderId, StockId = ord.StockId, BidQuantity = ord.Quantity, BidUnitPrice = ord.UnitPrice });

            if(order.OrderType == OrderValidity.IMMEDIATE.ToString())
            {
                
            }

            return null;

        }

        public Order SellShare(Order order)
        {
            return _orderService.PlaceOrder(order);
        }
        
    }
}

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
        private readonly IAccountService _accountService;
        private readonly IAccountTransactionHistoryService _accountTransactionHistoryService;

        public MemberOperation(ILoginService loginService, IOrderService orderService, IStockService stockService, IAccountService accountService, IAccountTransactionHistoryService accountTransactionHistoryService)
        {
            _loginService = loginService;
            _orderService = orderService;
            _stockService = stockService;
            _accountService = accountService;
            _accountTransactionHistoryService = accountTransactionHistoryService;
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

        public Account AddMoney(int memberId, decimal amount)
        {
           return _accountService.AddMoney(memberId, amount, TransactionStatus.MONEY_DEPOSITED);
        }

        public Account WithdrawMoney(int memberId, decimal amount)
        {
            return _accountService.DeductMoney(memberId, amount, TransactionStatus.MONEY_WITHDRAW);
        }

        public IEnumerable<AccountTransactionHistory> GetTransaction(int accountId)
        {
            return _accountTransactionHistoryService.GetTransaction(accountId);
        }

        public IEnumerable<Order> GetOrders(int memberId)
        {
            return _orderService.GetOrders(memberId);
        }

        public Order GetOrder(int orderId)
        {
            return _orderService.GetOrder(orderId);
        }

        public Account GetAccount(int memberId)
        {
          return  _accountService.GetAccount(memberId);
        }
    }
}

using DataLayer.Entities;
using DataLayer.Model;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Model.Misc;

namespace DataLayer
{
   public static class LiveStockDetailsProvider
    {
        public static List<Stock> LiveStocks { get; set; }
        private static readonly ApplicationContext _db = new ApplicationContext();
        public static Dictionary<int , SortedSet<StockTradeAsk>> AskedOrder = new Dictionary<int, SortedSet<StockTradeAsk>>();
        public static Dictionary<int , SortedSet<StockTradeBid>> BidedOrder = new Dictionary<int, SortedSet<StockTradeBid>>();
        public static List<Order> LiveOrders { get; set; }
        private static readonly OrderService _orderService = new OrderService();
        private static readonly AccountService _accountService = new AccountService();

        static LiveStockDetailsProvider()
        {
            Init();
        }

        private static void Init()
        {
            LiveStocks = new List<Stock>();
            foreach (var stock in _db.Stocks)
            {
                LiveStocks.Add(stock);
            }

            //foreach (var order in _db.Orders.Where(x => x.PurchaseTime.Date == DateTime.Now.Date))
            //{
            //    LiveOrders.Add(order);
            //}

            if(!RamdomPriceFluctuator.IsRunning)
                RamdomPriceFluctuator.Start();
        }

        public static void Refresh()
        {
            Init();
        }

        public static void FluctuateStockPrice(int priceDifference)
        {
            if (TimeSpan.Compare(DateTime.Now.TimeOfDay, StockMarketAvailablityProvider.StockMarketStopTime) == 0 || TimeSpan.Compare(DateTime.Now.TimeOfDay, StockMarketAvailablityProvider.StockMarketStopTime) == 1)
            {
                if (RamdomPriceFluctuator.IsRunning)
                    RamdomPriceFluctuator.Stop();
                return; // we can stop fluctuator here
            }
            foreach (var stock in LiveStocks)
            {
                stock.LivePrice = stock.InitialPrice + priceDifference;
            }
            ResolveOrders();
        }

        public static bool AddShareBid(StockTradeBid stockTradeBid)
        {
            if(!BidedOrder.ContainsKey(stockTradeBid.StockId))
            {
                SortedSet<StockTradeBid> sortedBidStock = new SortedSet<StockTradeBid>(new ByStocktradeBidDecending());
                sortedBidStock.Add(stockTradeBid);
            }
            else
            {
                BidedOrder[stockTradeBid.StockId].Add(stockTradeBid);
            }
            return true;
        }

        public static bool AddShareAsk(StockTradeAsk stockTradeAsk)
        {
            if (!AskedOrder.ContainsKey(stockTradeAsk.StockId))
            {
                SortedSet<StockTradeAsk> sortedAskStock = new SortedSet<StockTradeAsk>(new ByStocktradeAskAcending());
                sortedAskStock.Add(stockTradeAsk);
            }
            else
            {
                AskedOrder[stockTradeAsk.StockId].Add(stockTradeAsk);
            }
            return true;
        }

        public static bool RemoveShareBid(int orderId, int stockId)
        {
            var item = BidedOrder[stockId].Single(x => x.OrderId == orderId);
            BidedOrder[stockId].Remove(item);
            return true;
        }

        public static bool RemoveShareAsk(int orderId, int stockId)
        {
            var item = AskedOrder[stockId].Single(x => x.OrderId == orderId);
            AskedOrder[stockId].Remove(item);
            return true;
        }

        public static bool UpdateShareBid(int orderId, int stockId, int fulfilledQuantity)
        {
            var item = BidedOrder[stockId].Single(x => x.OrderId == orderId);
            item.BidQuantity -= fulfilledQuantity ;
            return true;
        }

        public static bool UpdateShareAsk(int orderId, int stockId, int fulfilledQuantity)
        {
            var item = AskedOrder[stockId].Single(x => x.OrderId == orderId);
            item.AskQuantity -= fulfilledQuantity;
            return true;
        }

        public static bool UpdateLiveOrder(int orderId, OrderStatus orderStatus, int fulfillment)
        {
            var item = LiveOrders.SingleOrDefault(x => x.OrderId == orderId);
            item.Status = orderStatus.ToString();
            item.FulfilledQuatity = fulfillment;
            return true;
        }

        public static bool RemoveLiveOrder(int orderId)
        {
            var item = LiveOrders.SingleOrDefault(x => x.OrderId == orderId);
            LiveOrders.Remove(item);
            return true;
        }

        public static bool CheckIfOrderCanBeResolved(Order order)
        {
            var matchs = AskedOrder[order.StockId].Where(x => x.AskUnitPrice <= order.UnitPrice);

            foreach (var askItem in matchs)
            {
                //Bid Fullfill
                if(askItem.AskQuantity >= (order.Quantity - order.FulfilledQuatity))
                {
                    order.FulfilledQuatity = order.Quantity;
                    if(askItem.AskQuantity == order.Quantity)
                    {
                        _accountService.DeductMoney(order.MemberId, order.Quantity * order.UnitPrice, TransactionStatus.STOCK_BUY);
                        _orderService.UpdateOrderStatus(order.OrderId, OrderStatus.COMPLETED.ToString());
                        UpdateLiveOrder(order.OrderId, OrderStatus.COMPLETED, order.FulfilledQuatity);

                        RemoveShareBid(order.OrderId, order.StockId);
                        
                    }
                    

                    askItem.AskQuantity -= order.Quantity;
                    if (askItem.AskQuantity == 0)
                    {
                        RemoveShareAsk(askItem.OrderId, askItem.StockId);
                        _accountService.AddMoney(askItem.MemberId, order.Quantity * order.UnitPrice, TransactionStatus.STOCK_SELL);
                        _orderService.UpdateOrderStatus(askItem.OrderId, OrderStatus.COMPLETED.ToString());
                        UpdateLiveOrder(askItem.OrderId, OrderStatus.COMPLETED, order.FulfilledQuatity);
                    }
                    else
                    {
                        _orderService.UpdateOrderFulfilment(askItem.OrderId, order.FulfilledQuatity);
                        _accountService.AddMoney(askItem.MemberId, askItem.AskQuantity * order.UnitPrice, TransactionStatus.STOCK_BUY);
                        _orderService.UpdateOrderStatus(askItem.OrderId, OrderStatus.PARTIALLY_COMPLETED.ToString());
                        UpdateLiveOrder(askItem.OrderId, OrderStatus.PARTIALLY_COMPLETED, order.FulfilledQuatity);
                        UpdateShareAsk(askItem.OrderId, askItem.StockId, order.FulfilledQuatity);
                    }
                    return true;
                }
                else if(order.OrderValidity == OrderValidity.IMMEDIATE.ToString())
                {
                    _orderService.UpdateOrderStatus(order.OrderId, OrderStatus.FAILED.ToString());
                    UpdateLiveOrder(order.OrderId, OrderStatus.FAILED, 0);
                    return false;
                }
                //partial fullfill Bid case
                else if(askItem.AskQuantity < (order.Quantity - order.FulfilledQuatity))
                {
                    //TODO
                    order.FulfilledQuatity += askItem.AskQuantity;

                    RemoveShareAsk(askItem.OrderId, askItem.StockId);
                    _orderService.UpdateOrderFulfilment(askItem.OrderId, askItem.AskQuantity);
                    
                    _orderService.UpdateOrderStatus(askItem.OrderId, OrderStatus.COMPLETED.ToString());
                    UpdateLiveOrder(askItem.OrderId, OrderStatus.COMPLETED, askItem.AskQuantity);

                    UpdateShareBid(order.OrderId, order.StockId, order.FulfilledQuatity);
                    _orderService.UpdateOrderFulfilment(order.OrderId, order.FulfilledQuatity);
                    _orderService.UpdateOrderStatus(order.OrderId, OrderStatus.PARTIALLY_COMPLETED.ToString());
                    UpdateLiveOrder(order.OrderId, OrderStatus.PARTIALLY_COMPLETED, order.FulfilledQuatity);
                }
            }
            return false;
        }

        public static void ResolveOrders()
        {
            foreach (var bidOrder in BidedOrder)
            {
                foreach (var orderItem in bidOrder.Value)
                {
                    CheckIfOrderCanBeResolved(LiveOrders.SingleOrDefault(x => x.OrderId == orderItem.OrderId));
                }
                
            }
               
        } 
    }


    public class ByStocktradeAskAcending : IComparer<StockTradeAsk>
    {
        decimal a, b;

        public int Compare(StockTradeAsk x, StockTradeAsk y)
        {
            a = x.AskUnitPrice;
            b = y.AskUnitPrice;
            
            if (a > b)
                return 1;
            if (a < b)
                return -1;
            else
                return 0;
            
        }
    }

    public class ByStocktradeBidDecending : IComparer<StockTradeBid>
    {
        decimal a, b;

        public int Compare(StockTradeBid x, StockTradeBid y)
        {
            a = x.BidUnitPrice;
            b = y.BidUnitPrice;

            if (a < b)
                return 1;
            if (a > b)
                return -1;
            else
                return 0;

        }
    }
}

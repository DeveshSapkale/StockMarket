
using DataLayer;
using DataLayer.Entities;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderService : IOrderService
    {
        private static readonly ApplicationContext _db = new ApplicationContext();

        public IEnumerable<Order> GetOrders(int memberId)
        {
            return _db.Orders.Where(x => x.MemberId == memberId);
        }

        public Order GetOrder(int orderId)
        {
            return _db.Orders.SingleOrDefault(x => x.OrderId == orderId);
        }

        public Order PlaceOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public Order UpdateOrderStatus(int orderId, string status)
        {
            var order = _db.Orders.SingleOrDefault(x => x.OrderId == orderId);
            order.Status = status;
            _db.SaveChanges();
            return order;
        }

        public Order UpdateOrderFulfilment(int orderId, int fulfilledQuantity)
        {
            var order = _db.Orders.SingleOrDefault(x => x.OrderId == orderId);
            order.FulfilledQuatity = fulfilledQuantity;
            _db.SaveChanges();
            return order;
        }
    }
}

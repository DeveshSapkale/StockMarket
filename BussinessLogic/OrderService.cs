using BussinessLogic.Interface;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class OrderService : IOrderService
    {
        private static readonly ApplicationContext _db = new ApplicationContext();

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
    }
}

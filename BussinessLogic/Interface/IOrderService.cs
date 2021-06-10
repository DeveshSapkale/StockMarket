using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interface
{
    public interface IOrderService
    {
        Order PlaceOrder(Order order);
        Order UpdateOrderStatus(int orderId, string status);
    }
}

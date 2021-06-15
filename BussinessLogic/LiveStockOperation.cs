using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
   public class LiveStockOperation
    {
        public LiveStockOperation()
        {
            if(!RamdomPriceFluctuator.IsRunning)
                RamdomPriceFluctuator.Start();
        }

        public static IEnumerable<Stock> GetLiveStockDetails()
        {
            return LiveStockDetailsProvider.LiveStocks;
        }

        public static IEnumerable<Order> GetLiveOrderDetails()
        {
            return LiveStockDetailsProvider.LiveOrders;
        }
    }
}

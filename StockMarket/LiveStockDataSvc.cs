using BussinessLogic;
using DataLayer.Entities;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace StockMarket
{
    [ServiceBehavior]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LiveStockDataSvc" in both code and config file together.
    public class LiveStockDataSvc : ILiveStockDataSvc
    {
        public IEnumerable<Order> GetLiveOrderDetails()
        {
            return LiveStockOperation.GetLiveOrderDetails();
        }

        IEnumerable<LiveStockDetails> ILiveStockDataSvc.GetLiveStockDetails()
        {
            return LiveStockOperation.GetLiveStockDetails();
        }
    }
}

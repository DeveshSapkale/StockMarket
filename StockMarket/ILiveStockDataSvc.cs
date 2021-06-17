using DataLayer.Entities;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StockMarket
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILiveStockDataSvc" in both code and config file together.
    [ServiceContract]
    public interface ILiveStockDataSvc
    {
        [OperationContract]
        IEnumerable<LiveStockDetails> GetLiveStockDetails();

        [OperationContract]
        IEnumerable<Order> GetLiveOrderDetails();
    }
}

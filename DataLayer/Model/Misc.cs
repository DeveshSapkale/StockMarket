using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public static class Misc
    {
        public enum OrderStatus
        {
            PENDING,
            COMPLETED,
            INCOMPLETE,
            PARTIALLY_COMPLETED,
            FAILED,
            CANCELLED
        }

        public enum OrderValidity
        {
            DAY,
            IMMEDIATE
        }

        public enum TransactionStatus
        {
            STOCK_BUY,
            STOCK_SELL,
            MONEY_DEPOSITED,
            MONEY_WITHDRAW
        }
    }
}

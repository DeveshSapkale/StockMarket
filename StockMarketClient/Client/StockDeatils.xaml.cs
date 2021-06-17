using DataLayer.Entities;
using StockMarketClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockMarketClient.Client
{
    /// <summary>
    /// Interaction logic for StockDeatils.xaml
    /// </summary>
    public partial class StockDetails : Window
    {
        StockViewModel stockViewModel = new StockViewModel();
        private readonly MemberHolding _memberHolding;
        public StockDetails(Stock stock, MemberHolding memberHolding = null)
        {
            InitializeComponent();
            stockViewModel = SimplePooling.LiveStocks.SingleOrDefault(x => x.Id == stock.Id);
            DataContext = stockViewModel;
            _memberHolding = memberHolding;
        }

        private void btnBuyShare_Click(object sender, RoutedEventArgs e)
        {
            BuySellStock buySellStock = new BuySellStock(this, _memberHolding, false);
        }

        private void btnSellShare_Click(object sender, RoutedEventArgs e)
        {
            BuySellStock buySellStock = new BuySellStock(this, _memberHolding, false);
        }
    }
}

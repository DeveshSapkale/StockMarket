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
    public partial class StockDeatils : Window
    {
        StockViewModel stockViewModel = new StockViewModel();
        public StockDeatils()
        {
            InitializeComponent();
            stockViewModel = SimplePooling.LiveStocks[0];
            DataContext = stockViewModel;
        }
    }
}

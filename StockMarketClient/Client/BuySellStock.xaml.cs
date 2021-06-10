using DataLayer.Entities;
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
    /// Interaction logic for BuySellStock.xaml
    /// </summary>
    public partial class BuySellStock : Window
    {
        private readonly Window _parentWindow;
        public BuySellStock(Window parent, MemberHolding memberHolding, bool isSellingMode)
        {
            InitializeComponent();
            _parentWindow = parent;
            lblShareName.Content = memberHolding.CompanyName;
            cbxExchange.Items.Add("NYSE");
            cbxPriceType.Items.Add("Market");
            cbxPriceType.Items.Add("Limit");
            cbxOrderValidity.Items.Add("DAY");
            cbxOrderValidity.Items.Add("IMMEDIATE");
            
            if (isSellingMode)
            {
                lblShareOperation.Content = "Sell Share";
                btnBuySell.Content = "Verify Sell";
                lblAvailableAmount.Visibility = Visibility.Hidden;
                lblShareCount.Content = $"{memberHolding.Quantity} shares available";
            }
            else
            {
                lblShareCount.Visibility = Visibility.Hidden;
                lblAvailableAmount.Content = $"Amount: {memberHolding.InvestedAmount}";
                lblShareOperation.Content = "Buy Shares";
                btnBuySell.Content = "Buy";
            }
        }

        private void btnBackToDashBoard_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _parentWindow.Show();
        }

        private void btnBuySell_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

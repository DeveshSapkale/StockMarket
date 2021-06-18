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
using static DataLayer.Model.Misc;
using ServiceReference = StockMarketClient.StockMarketServiceReference;

namespace StockMarketClient.Client
{
    /// <summary>
    /// Interaction logic for BuySellStock.xaml
    /// </summary>
    public partial class BuySellStock : Window
    {
        private readonly Window _parentWindow;
        private readonly bool _isSellingMode;
        private readonly MemberHolding _memberHolding;
        public BuySellStock(Window parent, MemberHolding memberHolding, bool isSellingMode)
        {
            InitializeComponent();
            _parentWindow = parent;
            _isSellingMode = isSellingMode;
            lblShareName.Content = memberHolding.CompanyName;
            _memberHolding = memberHolding;
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
                lblAvailableAmount.Content = $"Available Amount: {memberHolding.InvestedAmount}";
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
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            int orderShareCount = 0;
            decimal orderSharePrice = 0;
            if (int.TryParse(txtBuySellStockCount.Text, out orderShareCount) && decimal.TryParse(txtUnitSharePrice.Text, out orderSharePrice))
            {
                Order order = new Order
                {
                    MemberId = _memberHolding.MemberId,
                    OrderType = _isSellingMode ? OrderType.SELL.ToString() : OrderType.BUY.ToString(),
                    OrderValidity = cbxOrderValidity.SelectedItem.ToString(),
                    PurchaseTime = DateTime.Now,
                    Quantity = orderShareCount,
                    StockId = _memberHolding.StockId,
                    UnitPrice = orderSharePrice
                };

                try
                {
                    if (_isSellingMode)
                    {
                        serviceReference.SellShare(order);
                    }
                    else
                    {
                        serviceReference.BuyShare(order);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Fill all required fields");
            }
            
        }

        private void txtUnitSharePrice_LostFocus(object sender, RoutedEventArgs e)
        {
            int orderShareCount = 0;
            decimal orderSharePrice = 0;
            if(int.TryParse(txtBuySellStockCount.Text, out orderShareCount) && decimal.TryParse(txtUnitSharePrice.Text, out orderSharePrice))
            {
                lblOrderAmount.Content = orderShareCount * orderSharePrice;
            }
            else
            {
                lblOrderAmount.Content = 0;
            }
        }

        private void txtBuySellStockCount_LostFocus(object sender, RoutedEventArgs e)
        {
            int orderShareCount = 0;
            decimal orderSharePrice = 0;
            if (int.TryParse(txtBuySellStockCount.Text, out orderShareCount) && decimal.TryParse(txtUnitSharePrice.Text, out orderSharePrice))
            {
                lblOrderAmount.Content = orderShareCount * orderSharePrice;
            }
            else
            {
                lblOrderAmount.Content = 0;
            }
        }
    }
}

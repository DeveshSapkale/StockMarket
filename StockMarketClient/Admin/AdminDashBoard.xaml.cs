using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using ServiceReference = StockMarketClient.StockMarketServiceReference;

namespace StockMarketClient.Admin
{
    /// <summary>
    /// Interaction logic for AdminDashBoard.xaml
    /// </summary>
    public partial class AdminDashBoard : Window
    {
        
        public AdminDashBoard()
        {
            InitializeComponent();
            PopulateExchangeList();
            LoadMarketOffDates();
          
        }

        private void txtInitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            SetMarketCapitalization();
        }

        private void txtVolume_KeyDown(object sender, KeyEventArgs e)
        {
            SetMarketCapitalization();
        
        }

        private void btnCreateStock_Click(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock();
            stock.CompanyName = txtCompany.Text;
            stock.Exchange = cbxExchange.SelectedItem.ToString();
            stock.InitialPrice = Convert.ToDecimal(txtInitPrice.Text);
            stock.Volume = Convert.ToInt32(txtVolume.Text);
            stock.Symbol = txtSymbol.Text;
            stock.CreationDate = DateTime.Now;

            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            serviceReference.CreateStockAsync(stock);
        }

        private void SetMarketCapitalization()
        {
            decimal volume = 0;
            decimal initPrice = 0;
            initPrice = (string.IsNullOrEmpty(txtInitPrice.Text) ? 0 : Convert.ToDecimal(txtInitPrice.Text ?? "0"));
            volume = (string.IsNullOrEmpty(txtVolume.Text) ? 0 : Convert.ToDecimal(txtVolume.Text ?? "0"));
            lblMktCap.Content = initPrice * volume;

        }

        private void LoadMarketOffDates()
        {
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            MarketDateOffGrid.ItemsSource = serviceReference.GetStockMarketOffDates(); 
        }

        private void btnDeleteDateOff_ClickEvents(object sender, RoutedEventArgs e)
        {
            StockMarketOffDates stockMarketOffDates = MarketDateOffGrid.SelectedItem as StockMarketOffDates;
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            if (serviceReference.Delete(stockMarketOffDates.StockMarketOffDatesId))
            {
                MarketDateOffGrid.ItemsSource = null;
                MarketDateOffGrid.ItemsSource = serviceReference.GetStockMarketOffDates();
            }
        }

        private void PopulateExchangeList()
        {
            cbxExchange.Items.Add("NYSE");
        }

        private void btnAddDateOff_Click(object sender, RoutedEventArgs e)
        {
            StockMarketOffDates stockMarketOffDates = new StockMarketOffDates();
            stockMarketOffDates.Comment = txtComment.Text;
            stockMarketOffDates.OffDate = (DateTime)(clnDatePicker.SelectedDate);
            stockMarketOffDates.CreationDate = DateTime.Now;
            stockMarketOffDates.ModifiedDate = DateTime.Now;
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            var result =  serviceReference.Add(stockMarketOffDates);
            if(result.StockMarketOffDatesId > 0)
            {
                MarketDateOffGrid.ItemsSource = null;
                MarketDateOffGrid.ItemsSource = serviceReference.GetStockMarketOffDates();
            }
               
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDateOff.Text = clnDatePicker.SelectedDate.ToString();
        }
    }
}

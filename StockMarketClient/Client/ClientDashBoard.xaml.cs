using DataLayer.Entities;
using StockMarketClient.Misc;
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

namespace StockMarketClient.Client
{
    /// <summary>
    /// Interaction logic for ClientDashBoard.xaml
    /// </summary>
    public partial class ClientDashBoard : Window
    {
        int pageIndex = 1;
        private int numberOfRecPerPage = 5;
        //To check the paging direction according to use selection.
        private enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        List<MemberHolding> myHoldings = new List<MemberHolding>();

        public ClientDashBoard()
        {
            InitializeComponent();
            cbnumRec.Items.Add("10");
            cbnumRec.Items.Add("20");
            cbnumRec.Items.Add("30");
            cbnumRec.Items.Add("50");
            cbnumRec.Items.Add("100");
            cbnumRec.SelectedItem = 10;
            this.Loaded += HoldingsTab_Loaded;
        }

        private void HoldingsTab_Loaded(object sender, RoutedEventArgs e)
        {
            myHoldings = GetHoldings().ToList();
            holdingsGrid.ItemsSource = myHoldings.Take(numberOfRecPerPage);
            int count = myHoldings.Take(numberOfRecPerPage).Count();
            lblPageInfo.Content = count + " of " + myHoldings.Count;
            //txtProgress.Text = SimplePooling.LiveStocks.Count > 0 ? SimplePooling.LiveStocks[0].LivePrice.ToString(): "";
        }

        private IEnumerable<MemberHolding> GetHoldings()
        {
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();

           return serviceReference.GetMemberHoldings(1);
        }

        private void Navigate(int mode)
        {
            int count;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    if (myHoldings.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (myHoldings.Skip(pageIndex *
                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            holdingsGrid.ItemsSource = null;
                            holdingsGrid.ItemsSource = myHoldings.Skip((pageIndex *
                            numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (myHoldings.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            holdingsGrid.ItemsSource = null;
                            holdingsGrid.ItemsSource = myHoldings.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (myHoldings.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        lblPageInfo.Content = count + " of " + myHoldings.Count;
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                        btnLast.IsEnabled = false;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        holdingsGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            holdingsGrid.ItemsSource = myHoldings.Take(numberOfRecPerPage);
                            count = myHoldings.Take(numberOfRecPerPage).Count();
                            lblPageInfo.Content = count + " of " + myHoldings.Count;
                        }
                        else
                        {
                            holdingsGrid.ItemsSource = myHoldings.Skip
                            (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, myHoldings.Count);
                            lblPageInfo.Content = count + " of " + myHoldings.Count;
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                        btnFirst.IsEnabled = false;
                    }
                    break;

                case (int)PagingMode.First:
                    pageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;
                case (int)PagingMode.Last:
                    pageIndex = (myHoldings.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;

                case (int)PagingMode.PageCountChange:
                    pageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(cbnumRec.SelectedItem);
                    holdingsGrid.ItemsSource = null;
                    holdingsGrid.ItemsSource = myHoldings.Take(numberOfRecPerPage);
                    count = (myHoldings.Take(numberOfRecPerPage)).Count();
                    lblPageInfo.Content = count + " of " + myHoldings.Count;
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    break;
            }
        }

        private void btnBuyStock_ClickEvents(object sender, RoutedEventArgs e)
        {
            MemberHolding memberHolding = holdingsGrid.SelectedItem as MemberHolding;
            BuySellStock buySellStock = new BuySellStock(this, memberHolding, false);
            this.Hide();
            buySellStock.Show();
        }
        private void btnViewStock_ClickEvents(object sender, RoutedEventArgs e)
        {
           var stock =  stockSearchGrid.SelectedItem as Stock;
           var memberHolding = myHoldings.SingleOrDefault(x => x.StockId == stock.Id);
            StockDetails stockDetails = new StockDetails(stock, memberHolding);
            stockDetails.Show();
            
        }

        private void btnSellStock_ClickEvent(object sender, RoutedEventArgs e)
        {
            MemberHolding memberHolding = holdingsGrid.SelectedItem as MemberHolding;
            BuySellStock buySellStock = new BuySellStock(this, memberHolding, true);
            this.Hide();
            buySellStock.Show();
        }

        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.First);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);

        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Last);
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int)PagingMode.PageCountChange);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnSearchStock_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference.StockMarketClient serviceReference = new ServiceReference.StockMarketClient();
            var shares = serviceReference.GetSharesByName(txtSearchStock.Text.ToString());
            stockSearchGrid.ItemsSource = shares;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SimplePooling.Stop();
        }

        private void btnAddMoney_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWithdrawMoney_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextCash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevCash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFirstCash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLastCash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbCashNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

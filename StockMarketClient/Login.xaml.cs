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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockMarketClient.Admin;
using StockMarketClient.Client;
using ServiceReference = StockMarketClient.StockMarketServiceReference;
namespace StockMarketClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stockService = new ServiceReference.StockMarketClient();

            var member = stockService.Login(txtUsername.Text, txtPassword.Text);
            if (member == null)
            {
                lblError.Content = "Login Failed try with valid credential";
                return;
            }
            this.Hide();
            var isAdmin = (member.IsAdmin == true);
            if (isAdmin)
            {
                AdminDashBoard adminDashBoard = new AdminDashBoard();
                adminDashBoard.Show();
            }
            else
            {
                ClientDashBoard clientDashBoard = new ClientDashBoard();
                SimplePooling.Start();
                clientDashBoard.Show();
            }
        }
    }
}

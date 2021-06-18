using DataLayer.Entities;
using StockMarketClient.Admin;
using StockMarketClient.Client;
using ServiceReference = StockMarketClient.StockMarketServiceReference;
using System;
using System.Windows;
using System.ServiceModel;

namespace StockMarketClient
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Window _parentWindow;
        public Registration(Window parent)
        {
            InitializeComponent();
            _parentWindow = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = new Member();
                var service = new ServiceReference.StockMarketClient();

                member.FirstName = txtFirstName.Text;
                member.LastName = txtLastname.Text;
                member.EmailId = txtEmail.Text;
                member.UserName = txtUsername.Text;
                member.Password = txtPassword.Text;
                var memberResult = service.Register(member);
                if (memberResult == null || memberResult.Id == 0)
                {
                    return;
                }
                this.Hide();
                _parentWindow.Show();
            }
            catch (Exception ex)
            {

            }


        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool newVal = (cbxIsSame.IsChecked == true);
            if (newVal)
                txtUsername.Text = txtEmail.Text;
            else
                txtUsername.Text = string.Empty;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _parentWindow.Show();
        }
    }
}

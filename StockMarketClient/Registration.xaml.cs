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
        public Registration()
        {
            InitializeComponent();
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
                service.Register(member);
                if (member == null || member.Id == 0)
                {
                    return;
                }
                this.Hide();
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
    }
}

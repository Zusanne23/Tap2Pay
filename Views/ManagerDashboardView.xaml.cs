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


namespace Tap2PaySystem.Views
{
    public partial class ManagerDashboardView : Window
    {
        public ManagerDashboardView()
        {
            InitializeComponent();
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e)
        {
            POSView pos = new POSView();
            pos.Show();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView registration = new RegistrationView();
            registration.Show();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("User Management Module");
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InventoryView inventory = new InventoryView();
            inventory.Show();

            // Optional:
            // this.Hide();
        }

        private void btnTopUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Top Up Module");
        }

        private void btnTransactionHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Transaction History Module");
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reports Module");
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
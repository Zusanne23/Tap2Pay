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
    public partial class CashierDashboardView : Window
    {
        public CashierDashboardView()
        {
            InitializeComponent();
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("POS Module");
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Inventory Module");
        }

        private void btnTransactionHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Transaction History Module");
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
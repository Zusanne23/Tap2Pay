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

namespace Tap2PayAdmin.Views
{
    public partial class CashierDashboardView : Window
    {
        public CashierDashboardView()
        {
            InitializeComponent();

            if (Session.CurrentUser != null)
            {
                lblCashier.Text = $"Logged in as: {Session.CurrentUser.FullName}";
            }
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e)
        {
            POSView pos = new POSView();
            pos.Show();
            Close();
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InventoryView inventory = new InventoryView();
            inventory.Show();
            Close();
        }

        private void btnTransactionHistory_Click(object sender, RoutedEventArgs e)
        {
            TransactionHistoryView history = new TransactionHistoryView();
            history.Show();
            Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Session.CurrentUser = null;

            LoginView login = new LoginView();
            login.Show();
            Close();
        }
    }
}
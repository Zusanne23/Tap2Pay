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
using System.Linq;
using Tap2PaySystem.Services;

namespace Tap2PaySystem.Views
{
    public partial class TransactionHistoryView : Window
    {
        private readonly TransactionHistoryService service =
            new TransactionHistoryService();

        public TransactionHistoryView()
        {
            InitializeComponent();

            LoadTransactions();
        }

        private void LoadTransactions()
        {
            dgTransactions.ItemsSource = service.GetTransactions();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgTransactions.ItemsSource = service
                .GetTransactions()
                .Where(x =>
                    x.FullName.ToLower().Contains(keyword) ||
                    x.RFIDUID.ToLower().Contains(keyword) ||
                    x.TransactionId.ToString().Contains(keyword))
                .ToList();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboardView manager = new ManagerDashboardView();
            manager.Show();
            this.Close();
        }

    }
}
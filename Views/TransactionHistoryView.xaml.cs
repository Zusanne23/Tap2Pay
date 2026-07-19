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
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
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
            var transactions = service.GetAllTransactions();

            dgTransaction.ItemsSource = transactions;

            txtTotalTransactions.Text =
                $"Total Transactions: {transactions.Count}";

            txtTotalSales.Text =
                $"Total Sales: ₱{transactions.Sum(x => x.TotalAmount):N2}";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filtered = service
                .GetAllTransactions()
                .Where(x =>
                    x.CustomerName.ToLower().Contains(keyword) ||
                    x.OrderPurchased.ToLower().Contains(keyword))
                .ToList();

            dgTransaction.ItemsSource = filtered;

            txtTotalTransactions.Text =
                $"Total Transactions: {filtered.Count}";

            txtTotalSales.Text =
                $"Total Sales: ₱{filtered.Sum(x => x.TotalAmount):N2}";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Session.CurrentUser.Role == "Manager")
            {
                new ManagerDashboardView().Show();
            }
            else
            {
                new CashierDashboardView().Show();
            }

            Close();
        }
    }
}
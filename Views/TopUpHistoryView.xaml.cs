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
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
{
    public partial class TopUpHistoryView : Window
    {
        private readonly TopUpHistoryService service =
            new TopUpHistoryService();

        public TopUpHistoryView()
        {
            InitializeComponent();

            LoadTopUpHistory();
        }

        private void LoadTopUpHistory()
        {
            dgTopUpHistory.ItemsSource = service.GetTopUpHistory();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgTopUpHistory.ItemsSource = service
                .GetTopUpHistory()
                .Where(x =>
                    (x.FullName ?? "").ToLower().Contains(keyword) ||
                    (x.RFIDUID ?? "").ToLower().Contains(keyword) ||
                    x.TopUpId.ToString().Contains(keyword) ||
                    x.UserId.ToString().Contains(keyword))
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
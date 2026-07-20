using System;
using System.Collections.Generic;
using System.Linq;
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
using Tap2PayAdmin.Views;

namespace Tap2PayAdmin.Views
{
    public partial class ActivityLogsView : Window
    {
        private readonly ActivityLogService service =
            new ActivityLogService();

        public ActivityLogsView()
        {
            InitializeComponent();

            LoadLogs();
        }

        private void LoadLogs()
        {
            var logs = service.GetAllLogs();

            dgLogs.ItemsSource = logs;

            txtTotalLogs.Text =
                $"Total Logs : {logs.Count}";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            var logs = service
                .GetAllLogs()
                .Where(x =>
                    x.FullName.ToLower().Contains(keyword) ||
                    x.Action.ToLower().Contains(keyword) ||
                    x.Details.ToLower().Contains(keyword))
                .ToList();

            dgLogs.ItemsSource = logs;

            txtTotalLogs.Text =
                $"Total Logs : {logs.Count}";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new ManagerDashboardView().Show();

            Close();
        }
    }
}
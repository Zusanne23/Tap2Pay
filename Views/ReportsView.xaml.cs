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
    public partial class ReportsView : Window
    {
        private readonly ReportsService reportsService = new ReportsService();

        public ReportsView()
        {
            InitializeComponent();

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                var summary = reportsService.GetSummary();

                txtUsers.Text = summary.TotalUsers.ToString();
                txtTransactions.Text = summary.TotalTransactions.ToString();
                txtSales.Text = "₱ " + summary.TotalSales.ToString("N2");
                txtTopUp.Text = "₱ " + summary.TotalTopUp.ToString("N2");

                dgSales.ItemsSource = reportsService.GetSalesReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboardView manager = new ManagerDashboardView();
            manager.Show();
            Close();
        }
    }
}

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
using Tap2PayKiosk.Models;

namespace Tap2PayKiosk.Views
{
    public partial class CashQueueView : Window
    {
        public CashQueueView(List<CartItem> cart)
        {
            InitializeComponent();

            dgOrder.ItemsSource = cart;

            lblTotal.Text = $"TOTAL : ₱{cart.Sum(x => x.Amount):N2}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please pay at the cashier.");

            new KioskHomeView().Show();

            Close();
        }
    }
}
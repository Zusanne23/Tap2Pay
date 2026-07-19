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
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Views
{
    public partial class PaymentSuccessView : Window
    {
        public PaymentSuccessView(
            string customerName,
            string rfid,
            decimal total,
            decimal remainingBalance,
            List<CartItem> items)
        {
            InitializeComponent();

            txtName.Text = customerName;
            txtRFID.Text = rfid;
            txtTotal.Text = "₱ " + total.ToString("N2");
            txtRemainingBalance.Text = "₱ " + remainingBalance.ToString("N2");

            lvReceipt.ItemsSource = items;
        }

        private void btnNewTransaction_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
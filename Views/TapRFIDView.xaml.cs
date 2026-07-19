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
    public partial class TapRFIDView : Window
    {
        public TapRFIDView()
        {
            InitializeComponent();
        }

        private void btnRFID_Click(object sender, RoutedEventArgs e)
        {
            PaymentSuccessView payment = new PaymentSuccessView(
                "Juan Dela Cruz",
                "2023-00145",
                60.00m,
                240.00m,
                new List<CartItem>()
            );

            payment.ShowDialog();

            this.Close();
        }
    }
}
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
    public partial class TapRFIDView : Window
    {
        public TapRFIDView()
        {
            InitializeComponent();
        }

        private void btnRFID_Click(object sender, RoutedEventArgs e)
        {
            PaymentSuccessView payment = new PaymentSuccessView();
            payment.Show();

            this.Close();
        }
    }
}
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

namespace Tap2PayKiosk.Views
{
    public partial class CheckBalanceView : Window
    {
        public CheckBalanceView()
        {
            InitializeComponent();

            Loaded += (s, e) => txtRFID.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new KioskHomeView().Show();
            Close();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            new KioskHomeView().Show();
            Close();
        }

        private void txtRFID_KeyDown(object sender,
            System.Windows.Input.KeyEventArgs e)
        {
           
        }
    }
}
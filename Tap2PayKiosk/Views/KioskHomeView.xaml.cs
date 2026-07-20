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
    public partial class KioskHomeView : Window
    {
        public KioskHomeView()
        {
            InitializeComponent();
        }

        private void btnOrderFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderView order = new OrderView();
                order.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCheckBalance_Click(object sender, RoutedEventArgs e)
        {
            CheckBalanceView balance = new CheckBalanceView();
            balance.Show();
            Close();
        }
    }
}


using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tap2PaySystem.Data;

namespace Tap2PaySystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (DatabaseTest.TestConnection())
            {
                MessageBox.Show("Database Connected Successfully!");
            }
            else
            {
                MessageBox.Show("Failed to Connect Database!");
            }
        }
    }
}
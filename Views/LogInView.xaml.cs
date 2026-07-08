using System.Windows;
using Tap2PaySystem.Models;
using Tap2PaySystem.Services;

namespace Tap2PaySystem.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginService loginService = new LoginService();

        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            User user = loginService.Login(username, password);

            if (user != null)
            {
                if (user.Role == "Manager")
                {
                    ManagerDashboardView dashboard = new ManagerDashboardView();
                    dashboard.Show();
                    this.Close();
                }
                else if (user.Role == "Cashier")
                {
                    CashierDashboardView dashboard = new CashierDashboardView();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unknown user role.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password",
                                "Login Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
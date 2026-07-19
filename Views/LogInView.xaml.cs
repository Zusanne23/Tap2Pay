using System.Windows;
using System.Windows.Input;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
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
                Session.CurrentUser = user;

                if (user.Role == "Manager")
                {
                    ManagerDashboardView dashboard = new ManagerDashboardView();
                    dashboard.Show();
                    Close();
                }
                else if (user.Role == "Cashier")
                {
                    CashierDashboardView dashboard = new CashierDashboardView();
                    dashboard.Show();
                    Close();
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, new RoutedEventArgs());
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }
        }

    }
}
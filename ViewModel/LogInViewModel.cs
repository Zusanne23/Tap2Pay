using System.Windows;
using System.Windows.Input;
using Tap2PayAdmin.Commands;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.ViewModel
{
    public class LogInViewModel : BaseViewModel
    {
        private readonly LoginService loginService = new LoginService();

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LogInViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object obj)
        {
            User user = loginService.Login(Username, Password);

            if (user != null)
            {
                MessageBox.Show($"Welcome {user.FullName}");

                if (user.Role == "Manager")
                {
                    MessageBox.Show("Manager Login Successful");
                }
                else if (user.Role == "Cashier")
                {
                    MessageBox.Show("Cashier Login Successful");
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
    }
}
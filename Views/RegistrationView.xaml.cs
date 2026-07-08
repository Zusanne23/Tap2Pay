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
using Tap2PaySystem.Models;
using Tap2PaySystem.Services;

namespace Tap2PaySystem.Views
{
    public partial class RegistrationView : Window
    {
        private readonly UserService userService = new UserService();

        public RegistrationView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User
                {
                    FullName = txtFullName.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Password,
                    RFIDUID = txtRFID.Text,
                    Role = ((ComboBoxItem)cbRole.SelectedItem).Content.ToString(),
                    Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString(),
                    Balance = decimal.Parse(txtBalance.Text)
                };

                userService.AddUser(user);

                MessageBox.Show("User registered successfully!");

                // Clear fields
                txtFullName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
                txtRFID.Clear();
                txtBalance.Text = "0.00";
                cbRole.SelectedIndex = -1;
                cbStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
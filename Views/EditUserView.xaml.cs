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
    public partial class EditUserView : Window
    {
        private readonly UserService userService = new UserService();

        private User currentUser;

        public EditUserView(User user)
        {
            InitializeComponent();

            currentUser = user;

            txtFullName.Text = user.FullName;
            txtUsername.Text = user.Username;
            txtPassword.Password = user.Password;
            txtBalance.Text = user.Balance.ToString();

            foreach (ComboBoxItem item in cbRole.Items)
            {
                if (item.Content.ToString() == user.Role)
                {
                    cbRole.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString() == user.Status)
                {
                    cbStatus.SelectedItem = item;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentUser.FullName = txtFullName.Text;
            currentUser.Username = txtUsername.Text;
            currentUser.Password = txtPassword.Password;
            currentUser.Balance = decimal.Parse(txtBalance.Text);
            currentUser.Role = ((ComboBoxItem)cbRole.SelectedItem).Content.ToString();
            currentUser.Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

            userService.UpdateUser(currentUser);

            MessageBox.Show("User updated successfully.");

            DialogResult = true;
            Close();
        }
    }
}
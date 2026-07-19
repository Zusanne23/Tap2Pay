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
using System.Collections.ObjectModel;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
{
    public partial class UserManagementView : Window
    {
        private readonly UserService userService = new UserService();

        public UserManagementView()
        {
            InitializeComponent();

            LoadUsers();
        }

        private void LoadUsers()
        {
            dgUsers.ItemsSource = userService.GetAllUsers();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboardView manager = new ManagerDashboardView();
            manager.Show();
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgUsers.ItemsSource = userService.GetAllUsers().FindAll(x =>
                x.FullName.ToLower().Contains(keyword) ||
                x.Username.ToLower().Contains(keyword) ||
                x.Role.ToLower().Contains(keyword) ||
                x.Status.ToLower().Contains(keyword));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            User selectedUser = (User)dgUsers.SelectedItem;

            EditUserView editWindow = new EditUserView(selectedUser);

            if (editWindow.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            User selectedUser = (User)dgUsers.SelectedItem;

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure you want to delete {selectedUser.FullName}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                userService.DeleteUser(selectedUser.UserId);

                MessageBox.Show("User deleted successfully.");

                LoadUsers();
            }
        }
    }
}
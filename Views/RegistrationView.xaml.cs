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

        private bool waitingForRFID = false;

        private string scannedRFID = "";

        public RegistrationView()
        {
            InitializeComponent();

            txtRFID.IsReadOnly = true;

            LoadUsers();
        }
        private void RegistrationView_Loaded(object sender, RoutedEventArgs e)
        {
            txtRFID.Focus();
        }
        private void LoadUsers()
        {
            dgUsers.ItemsSource = userService.GetAllUsers();
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

                txtRFID.IsReadOnly = true;

                LoadUsers();

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
        private void btnScanRFID_Click(object sender, RoutedEventArgs e)
        {
            waitingForRFID = true;

            txtRFID.Clear();
            txtRFIDScanner.Clear();

            MessageBox.Show(
                "Please tap the Student ID.",
                "RFID Scanner",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            txtRFIDScanner.Focus();
        }

        private void txtRFIDScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (!waitingForRFID)
                return;

            if (e.Key == Key.Enter)
            {
                txtRFID.Text = txtRFIDScanner.Text.Trim();

                txtRFIDScanner.Clear();

                waitingForRFID = false;

                MessageBox.Show(
                    "RFID scanned successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
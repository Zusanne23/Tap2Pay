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
using System;
using System.Linq;
using Tap2PaySystem.Models;
using Tap2PaySystem.Services;

namespace Tap2PaySystem.Views
{
    public partial class TopUpView : Window
    {
        private readonly TopUpService service = new TopUpService();

        private User selectedUser;

        private bool waitingForRFID = false;

        public TopUpView()
        {
            InitializeComponent();

            LoadUsers();
        }

        private void LoadUsers()
        {
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = service.GetAllUsers();
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
                return;

            selectedUser = (User)dgUsers.SelectedItem;

            txtBalance.Text = selectedUser.Balance.ToString("N2");
        }

        private void btnTopUp_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Invalid amount.");
                return;
            }

            decimal newBalance = selectedUser.Balance + amount;

            service.UpdateBalance(selectedUser.UserId, newBalance);

            service.AddTopUp(new TopUp
            {
                UserId = selectedUser.UserId,
                Amount = amount,
                TopUpDate = DateTime.Now
            });

            MessageBox.Show("Top Up Successful!");

            txtAmount.Clear();

            LoadUsers();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboardView manager = new ManagerDashboardView();
            manager.Show();
            Close();
        }

        private void btnScanRFID_Click(object sender, RoutedEventArgs e)
        {
            waitingForRFID = true;

            txtRFID.Clear();
            txtRFIDScanner.Clear();

            MessageBox.Show("Please tap the Student ID.");

            txtRFIDScanner.Focus();
        }

        private void txtRFIDScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (!waitingForRFID)
                return;

            if (e.Key == Key.Enter)
            {
                string uid = txtRFIDScanner.Text.Trim();

                txtRFID.Text = uid;

                txtRFIDScanner.Clear();

                waitingForRFID = false;

                var user = service.GetAllUsers()
                                  .FirstOrDefault(x => x.RFIDUID == uid);

                if (user == null)
                {
                    MessageBox.Show("RFID not registered.");
                    return;
                }

                selectedUser = user;

                txtBalance.Text = user.Balance.ToString("N2");

                dgUsers.SelectedItem = user;
                dgUsers.ScrollIntoView(user);

                MessageBox.Show("Student Found:\n\n" + user.FullName);
            }
        }
    }
}

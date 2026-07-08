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
    public partial class POSView : Window
    {
        private readonly InventoryService inventoryService = new InventoryService();
        private readonly TransactionService transactionService = new TransactionService();

        private List<CartItem> cart = new List<CartItem>();
        public POSView()
        {
            InitializeComponent();

            LoadInventory();
        }

        private void LoadInventory()
        {
            dgInventory.ItemsSource = inventoryService.GetAllItems();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Inventory item = (Inventory)button.DataContext;

            CartItem existing = cart.FirstOrDefault(x => x.InventoryId == item.InventoryId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    InventoryId = item.InventoryId,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    Quantity = 1
                });
            }

            dgCart.ItemsSource = null;
            dgCart.ItemsSource = cart;

            ComputeTotal();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Please add an item first.");
                return;
            }

            string paymentMethod = rbRFID.IsChecked == true ? "RFID" : "Cash";

            Transaction transaction = new Transaction
            {
                UserId = 1,
                TotalAmount = cart.Sum(x => x.Amount),
                PaymentMethod = paymentMethod,
                TransactionDate = DateTime.Now,
                Status = "Completed"
            };

            int transactionId = transactionService.AddTransaction(transaction);

            foreach (CartItem item in cart)
            {
                TransactionItem transactionItem = new TransactionItem
                {
                    TransactionId = transactionId,
                    InventoryId = item.InventoryId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Amount = item.Amount
                };

                transactionService.AddTransactionItem(transactionItem);

                inventoryService.DeductStock(item.InventoryId, item.Quantity);
            }

            MessageBox.Show("Payment Successful!");

            cart.Clear();

            dgCart.ItemsSource = null;

            ComputeTotal();

            LoadInventory();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Cancel current order?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                cart.Clear();

                dgCart.ItemsSource = null;

                ComputeTotal();

                lblUserId.Text = "User ID :";
                lblName.Text = "Name : Waiting for Customer";
                lblBalance.Text = "Balance : ₱0.00";
                lblRFID.Text = "🟡 Waiting for Payment...";
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgInventory.ItemsSource = inventoryService
                .GetAllItems()
                .Where(x => x.ItemName.ToLower().Contains(keyword))
                .ToList();
        }

        private void ComputeTotal()
        {
            decimal total = cart.Sum(x => x.Amount);

            lblTotal.Text = $"TOTAL : ₱{total:N2}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tap2PayKiosk.Models;
using Tap2PayKiosk.Services;

namespace Tap2PayKiosk.Views
{
    public partial class RFIDPaymentView : Window
    {
        private readonly UserService userService = new UserService();
        private readonly TransactionService transactionService = new TransactionService();

        private List<CartItem> cart;

        public RFIDPaymentView(List<CartItem> items)
        {
            InitializeComponent();

            cart = items;

            txtRFID.Focus();
        }

        private void txtRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            string rfid = txtRFID.Text.Trim();

            User user = userService.GetByRFID(rfid);

            if (user == null)
            {
                MessageBox.Show("RFID not found.");
                txtRFID.Clear();
                return;
            }

            decimal total = cart.Sum(x => x.Amount);

            if (user.Balance < total)
            {
                MessageBox.Show("Insufficient Balance.");
                txtRFID.Clear();
                return;
            }

            decimal remaining = user.Balance - total;

            userService.UpdateBalance(user.UserId, remaining);

            Transaction transaction = new Transaction
            {
                UserId = user.UserId,
                TotalAmount = total,
                PaymentMethod = "RFID",
                TransactionDate = DateTime.Now,
                Status = "Completed"
            };

            int transactionId = transactionService.AddTransaction(transaction);

            foreach (CartItem item in cart)
            {
                transactionService.AddTransactionItem(new TransactionItem
                {
                    TransactionId = transactionId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Amount = item.Amount
                });
            }

            MessageBox.Show(
                $"Payment Successful!\n\n" +
                $"Customer : {user.FullName}\n" +
                $"Remaining Balance : ₱{remaining:N2}");

            new KioskHomeView().Show();

            Close();
        }
    }
}
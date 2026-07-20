using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tap2PayKiosk.Model;
using Tap2PayKiosk.Models;
using Tap2PayKiosk.Services;

namespace Tap2PayKiosk.Views
{
    public partial class OrderView : Window
    {
        private readonly ProductService productService = new ProductService();
        private readonly TransactionService transactionService = new TransactionService();

        private readonly List<CartItem> cart = new();

        public OrderView()
        {
            InitializeComponent();

            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = productService.GetAllProducts();

            icProducts.ItemsSource = products;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)((Button)sender).Tag;

            CartItem existing = cart.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (existing != null)
                existing.Quantity++;
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ItemName = product.ProductName,
                    Category = product.Category,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            dgCart.ItemsSource = null;
            dgCart.ItemsSource = cart;

            ComputeTotal();
        }

        private void ComputeTotal()
        {
            decimal total = cart.Sum(x => x.Amount);

            lblTotal.Text = $"TOTAL : ₱{total:N2}";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            icProducts.ItemsSource = productService
                .GetAllProducts()
                .Where(p => p.ProductName.ToLower().Contains(keyword))
                .ToList();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (cbCategory.SelectedItem is not ComboBoxItem item)
                return;

            string category = item.Content.ToString();

            var products = productService.GetAllProducts();

            if (category != "All")
            {
                products = products
                    .Where(p => p.Category == category)
                    .ToList();
            }

            icProducts.ItemsSource = products;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cart.Clear();

            dgCart.ItemsSource = null;

            ComputeTotal();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new KioskHomeView().Show();
            Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Please select at least one item.");
                return;
            }

            if (rbRFID.IsChecked == true)
            {
                new RFIDPaymentView(cart).Show();
            }
            else
            {
                new CashQueueView(cart).Show();
            }

            Close();
        }
    }
}
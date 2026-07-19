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
using Tap2PayAdmin.Model;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
{
    public partial class POSView : Window
    {
        private readonly InventoryService inventoryService = new InventoryService();
        private readonly TransactionService transactionService = new TransactionService();
        private readonly ProductService productService = new ProductService();
        private readonly UserService userService = new UserService();

        private User currentCustomer;

        private List<CartItem> cart = new List<CartItem>();
        public POSView()
        {
            InitializeComponent();

            LoadProducts();
        }

        private void POSView_Loaded(object sender, RoutedEventArgs e)
        {
            txtRFID.Focus();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Product product = (Product)button.Tag;

            string size = rbWhole.IsChecked == true ? "Whole" : "Half";

            decimal price = product.Price;

            if (size == "Half")
            {
                price = product.Price / 2;
            }

            DependencyObject parent = button;

            while (parent != null && !(parent is Border))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is Border border)
            {
                StackPanel panel = border.Child as StackPanel;

                if (panel != null)
                {
                    foreach (UIElement child in panel.Children)
                    {
                        if (child is GroupBox groupBox)
                        {
                            StackPanel sp = groupBox.Content as StackPanel;

                            if (sp != null)
                            {
                                foreach (UIElement item in sp.Children)
                                {
                                    if (item is RadioButton rb && rb.IsChecked == true)
                                    {
                                        size = rb.Content.ToString();

                                        switch (size)
                                        {
                                            case "Half":
                                                price = product.Price / 2;
                                                break;

                                            case "Whole":
                                                price = product.Price;
                                                break;

                                            default:
                                                price = product.Price;
                                                break;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CartItem existing = cart.FirstOrDefault(x =>
                x.ProductId == product.ProductId &&
                x.Size == size);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ItemName = product.ProductName,
                    Category = product.Category,
                    Size = size,
                    Price = price,
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
                UserId = Session.CurrentUser.UserId,
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
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Amount = item.Amount
                };

                if (rbRFID.IsChecked == true)
                {
                    lblRFID.Text = "🟢 Please Tap RFID Card...";
                    txtRFID.Clear();
                    txtRFID.Focus();

                    return;
                }

                transactionService.AddTransactionItem(transactionItem);

            }

            decimal total = cart.Sum(x => x.Amount);

            string customerName = "Juan Dela Cruz";
            string rfid = "2023-00145";
            decimal remainingBalance = 240.00m;

            PaymentSuccessView receipt = new PaymentSuccessView(
                customerName,
                rfid,
                total,
                remainingBalance,
                cart.ToList());

            receipt.ShowDialog();

            cart.Clear();

            dgCart.ItemsSource = null;

            ComputeTotal();

            lblUserId.Text = "User ID :";
            lblName.Text = "Name : Waiting for Customer";
            lblBalance.Text = "Balance : ₱0.00";
            lblRFID.Text = "🟡 Waiting for Payment...";
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

            icProducts.ItemsSource = productService
                    .GetAllProducts()
                    .Where(x => x.ProductName.ToLower().Contains(keyword))
                    .ToList();
        }

        private void ComputeTotal()
        {
            decimal total = cart.Sum(x => x.Amount);

            lblTotal.Text = $"TOTAL : ₱{total:N2}";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Session.CurrentUser.Role == "Manager")
            {
                ManagerDashboardView manager = new ManagerDashboardView();
                manager.Show();
            }
            else if (Session.CurrentUser.Role == "Cashier")
            {
                CashierDashboardView cashier = new CashierDashboardView();
                cashier.Show();
            }

            this.Close();
        }

        private void LoadProducts()
        {
            if (icProducts == null)
            {
                MessageBox.Show("icProducts is NULL");
                return;
            }

            icProducts.ItemsSource = productService.GetAllProducts();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (icProducts == null)
                return;

            if (cbCategory.SelectedItem == null)
                return;

            string category = ((ComboBoxItem)cbCategory.SelectedItem).Content.ToString();

            var products = productService.GetAllProducts();

            if (category != "All")
            {
                products = products
                    .Where(x => x.Category == category)
                    .ToList();
            }

            icProducts.ItemsSource = products;
        }
        private void txtRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            string rfid = txtRFID.Text.Trim();

            txtRFID.Clear();

            ProcessRFIDPayment(rfid);

            txtRFID.Focus();
        }

        private void ProcessRFIDPayment(string rfid)
        {
            MessageBox.Show("RFID Detected: " + rfid);
        }
    }
}
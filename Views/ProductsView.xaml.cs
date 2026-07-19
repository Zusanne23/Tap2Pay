using System;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ProductsView : Window
    {
        private readonly ProductService productService = new ProductService();

        public ProductsView()
        {
            InitializeComponent();

            if (Session.CurrentUser.Role == "Cashier")
            {
                btnAdd.Visibility = Visibility.Collapsed;
                editColumn.Visibility = Visibility.Collapsed;
                deleteColumn.Visibility = Visibility.Collapsed;
            }

            LoadProducts();
        }

        private void LoadProducts()
        {
            dgProducts.ItemsSource = productService.GetAllProducts();

            txtTotalProducts.Text = productService.GetTotalProducts().ToString();
            txtAvailable.Text = productService.GetAvailableProducts().ToString();
            txtMeals.Text = productService.GetMealsCount().ToString();
            txtDrinks.Text = productService.GetDrinksCount().ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductView add = new AddProductView();
            add.Owner = this;
            add.ShowDialog();

            LoadProducts();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Product product = (Product)button.Tag;

            AddProductView edit = new AddProductView(product);

            if (edit.ShowDialog() == true)
            {
                LoadProducts();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Product product = (Product)button.Tag;

            MessageBoxResult result = MessageBox.Show(
                $"Delete '{product.ProductName}'?",
                "Delete Product",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                productService.DeleteProduct(product.ProductId);

                MessageBox.Show("Product deleted successfully!");

                LoadProducts();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgProducts.ItemsSource = productService
                .GetAllProducts()
                .Where(x => x.ProductName.ToLower().Contains(keyword))
                .ToList();
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

            Close();
        }
    }
}
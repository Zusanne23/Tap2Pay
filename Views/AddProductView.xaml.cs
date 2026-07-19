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
using Microsoft.Win32;

namespace Tap2PayAdmin.Views
{
    public partial class AddProductView : Window
    {
        private readonly ProductService productService = new ProductService();

        private bool isEdit = false;
        private Product currentProduct;
        private string imagePath = "";
        public AddProductView()
        {
            InitializeComponent();

            cmbCategory.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
        }

        public AddProductView(Product product)
        {
            InitializeComponent();

            isEdit = true;
            currentProduct = product;

            txtProductName.Text = product.ProductName;
            txtPrice.Text = product.Price.ToString();

            foreach (ComboBoxItem item in cmbCategory.Items)
            {
                if (item.Content.ToString() == product.Category)
                {
                    cmbCategory.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in cmbStatus.Items)
            {
                if (item.Content.ToString() == product.Status)
                {
                    cmbStatus.SelectedItem = item;
                    break;
                }
            }

            imagePath = product.ImagePath;

            if (!string.IsNullOrEmpty(imagePath))
            {
                imgPreview.Source = new BitmapImage(new Uri(imagePath));
            }

            btnSave.Content = "💾 Update";
            Title = "Edit Product";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEdit)
                {
                    Product product = new Product
                    {
                        ProductName = txtProductName.Text,
                        Category = ((ComboBoxItem)cmbCategory.SelectedItem).Content.ToString(),
                        Price = decimal.Parse(txtPrice.Text),
                        Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString(),
                        ImagePath = imagePath,
                    };

                    productService.AddProduct(product);

                    MessageBox.Show("Product added successfully!");
                }
                else
                {
                    currentProduct.ProductName = txtProductName.Text;
                    currentProduct.Category = ((ComboBoxItem)cmbCategory.SelectedItem).Content.ToString();
                    currentProduct.Price = decimal.Parse(txtPrice.Text);
                    currentProduct.Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();
                    currentProduct.ImagePath = imagePath;

                    productService.UpdateProduct(currentProduct);

                    MessageBox.Show("Product updated successfully!");
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                imagePath = dialog.FileName;

                imgPreview.Source = new BitmapImage(new Uri(imagePath));
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
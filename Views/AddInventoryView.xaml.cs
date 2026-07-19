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
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
{
    public partial class AddInventoryView : Window
    {
        private readonly InventoryService inventoryService = new InventoryService();

        private bool isEdit = false;
        private Inventory currentItem;

        public AddInventoryView()
        {
            InitializeComponent();

            cbStatus.SelectedIndex = 0;
        }
        public AddInventoryView(Inventory item)
        {
            InitializeComponent();

            isEdit = true;
            currentItem = item;

            txtItemName.Text = item.ItemName;
            txtPrice.Text = item.Price.ToString();
            txtStock.Text = item.Stock.ToString();
            dpExpiration.SelectedDate = item.ExpirationDate;

            foreach (ComboBoxItem status in cbStatus.Items)
            {
                if (status.Content.ToString() == item.Status)
                {
                    cbStatus.SelectedItem = status;
                    break;
                }
            }

            btnSave.Content = "💾 Update";
            Title = "Edit Inventory Item";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEdit)
                {
                    Inventory item = new Inventory
                    {
                        ItemName = txtItemName.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        Stock = int.Parse(txtStock.Text),
                        ExpirationDate = dpExpiration.SelectedDate.Value,
                        Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString()
                    };

                    inventoryService.AddItem(item);
                }
                else
                {
                    currentItem.ItemName = txtItemName.Text;
                    currentItem.Price = decimal.Parse(txtPrice.Text);
                    currentItem.Stock = int.Parse(txtStock.Text);
                    currentItem.ExpirationDate = dpExpiration.SelectedDate.Value;
                    currentItem.Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

                    inventoryService.UpdateItem(currentItem);

                    MessageBox.Show("Item updated successfully!");
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
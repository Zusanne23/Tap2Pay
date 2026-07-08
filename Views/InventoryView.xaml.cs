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
    public partial class InventoryView : Window
    {
        private readonly InventoryService inventoryService = new InventoryService();

        public InventoryView()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void LoadInventory()
        {
            dgInventory.ItemsSource = null;
            dgInventory.ItemsSource = inventoryService.GetAllItems();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inventory item = new Inventory
                {
                    ItemName = txtItemName.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Stock = int.Parse(txtStock.Text),
                    ExpirationDate = dpExpiration.SelectedDate.Value,
                    Status = cbStatus.Text
                };

                inventoryService.AddItem(item);

                MessageBox.Show("Item added successfully!");

                LoadInventory();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            try
            {
                Inventory item = (Inventory)dgInventory.SelectedItem;

                item.ItemName = txtItemName.Text;
                item.Price = decimal.Parse(txtPrice.Text);
                item.Stock = int.Parse(txtStock.Text);
                item.ExpirationDate = dpExpiration.SelectedDate.Value;
                item.Status = cbStatus.Text;

                inventoryService.UpdateItem(item);

                MessageBox.Show("Item updated successfully!");

                LoadInventory();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            Inventory item = (Inventory)dgInventory.SelectedItem;

            inventoryService.DeleteItem(item.InventoryId);

            MessageBox.Show("Item deleted successfully!");

            LoadInventory();
            ClearFields();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgInventory.ItemsSource = inventoryService
                .GetAllItems()
                .Where(x => x.ItemName.ToLower().Contains(keyword))
                .ToList();
        }

        private void dgInventory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgInventory.SelectedItem == null)
                return;

            Inventory item = (Inventory)dgInventory.SelectedItem;

            txtItemName.Text = item.ItemName;
            txtPrice.Text = item.Price.ToString();
            txtStock.Text = item.Stock.ToString();
            dpExpiration.SelectedDate = item.ExpirationDate;
            cbStatus.Text = item.Status;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboardView manager = new ManagerDashboardView();
            manager.Show();
            this.Close();
        }

        private void ClearFields()
        {
            txtItemName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtSearch.Clear();

            dpExpiration.SelectedDate = null;
            cbStatus.SelectedIndex = -1;

            dgInventory.SelectedItem = null;
        }
    }
}
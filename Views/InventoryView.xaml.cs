using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;

namespace Tap2PayAdmin.Views
{
    public partial class InventoryView : Window
    {
        private readonly InventoryService inventoryService = new InventoryService();
        private readonly ActivityLogService logService = new ActivityLogService();


        public InventoryView()
        {
            InitializeComponent();
            if (Session.CurrentUser.Role == "Cashier")
            {
                btnAdd.Visibility = Visibility.Collapsed;
            }

            LoadInventory();
            if (Session.CurrentUser.Role == "Cashier")
            {
                btnAdd.Visibility = Visibility.Collapsed;

                editColumn.Visibility = Visibility.Collapsed;
                deleteColumn.Visibility = Visibility.Collapsed;
            }

        }

        private void LoadInventory()
        {
            dgInventory.ItemsSource = inventoryService.GetAllItems();

            txtTotalProducts.Text = inventoryService.GetTotalProducts().ToString();
            txtAvailable.Text = inventoryService.GetAvailableProducts().ToString();
            txtLowStock.Text = inventoryService.GetLowStockProducts().ToString();
            txtOutStock.Text = inventoryService.GetOutOfStockProducts().ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddInventoryView add = new AddInventoryView();

            add.Owner = this;
            add.ShowDialog();

            LoadInventory();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Inventory item = (Inventory)button.Tag;

            AddInventoryView edit = new AddInventoryView(item);

            if (edit.ShowDialog() == true)
            {
                LoadInventory();
            }

            logService.AddLog(
                Session.CurrentUser.UserId,
                Session.CurrentUser.FullName,   
                Session.CurrentUser.Role,
                "Inventory",
                $"Added product: {item.ItemName}"
            );
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
{
    Button button = (Button)sender;

    Inventory item = (Inventory)button.Tag;

    MessageBoxResult result = MessageBox.Show(
        $"Delete '{item.ItemName}'?",
        "Delete Item",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

    if (result == MessageBoxResult.Yes)
    {
        inventoryService.DeleteItem(item.InventoryId);

        MessageBox.Show("Item deleted successfully!");

        LoadInventory();
    }
}

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            dgInventory.ItemsSource = inventoryService
                .GetAllItems()
                .Where(x => x.ItemName.ToLower().Contains(keyword))
                .ToList();
        }

        private void dgInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
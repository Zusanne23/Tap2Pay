using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository repository;

        public InventoryService()
        {
            repository = new InventoryRepository();
        }

        public List<Inventory> GetAllItems()
        {
            return repository.GetAllItems();
        }

        public void AddItem(Inventory item)
        {
            repository.AddItem(item);
        }

        public void UpdateItem(Inventory item)
        {
            repository.UpdateItem(item);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public void DeductStock(int inventoryId, int quantity)
        {
            repository.DeductStock(inventoryId, quantity);
        }
        public int GetTotalProducts()
        {
            return repository.GetTotalProducts();
        }

        public int GetAvailableProducts()
        {
            return repository.GetAvailableProducts();
        }

        public int GetLowStockProducts()
        {
            return repository.GetLowStockProducts();
        }

        public int GetOutOfStockProducts()
        {
            return repository.GetOutOfStockProducts();
        }

    }
}

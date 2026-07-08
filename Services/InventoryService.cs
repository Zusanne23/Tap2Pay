using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
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
       
    }
}

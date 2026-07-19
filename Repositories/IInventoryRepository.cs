using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public interface IInventoryRepository
    {
        List<Inventory> GetAllItems();

        void AddItem(Inventory item);

        void UpdateItem(Inventory item);

        void DeleteItem(int inventoryId);

        void DeductStock(int inventoryId, int quantity);

        int GetTotalProducts();

        int GetAvailableProducts();

        int GetLowStockProducts();

        int GetOutOfStockProducts();
    }
}
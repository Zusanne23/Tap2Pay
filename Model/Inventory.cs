using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Status { get; set; }
    }
}
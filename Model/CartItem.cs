using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PaySystem.Models
{
    public class CartItem
    {
        public int InventoryId { get; set; }

        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Amount
        {
            get { return Price * Quantity; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayKiosk.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public string ItemName { get; set; }

        public string Category { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Amount
        {
            get { return Price * Quantity; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public string ImagePath { get; set; }

        public string SelectedSize { get; set; } = "Whole";

        public bool IsMeal
        {
            get
            {
                return Category == "Meals";
            }
        }
    }
}
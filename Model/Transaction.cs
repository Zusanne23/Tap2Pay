using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int UserId { get; set; }

        public string CustomerName { get; set; }

        public string RFIDUID { get; set; }

        public string OrderPurchased { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Status { get; set; }

        public string FullName { get; set; }
    }
}
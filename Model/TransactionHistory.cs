using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class TransactionHistory
    {
        public int TransactionId { get; set; }

        public string CustomerName { get; set; }

        public string RFID { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Status { get; set; }
    }
}
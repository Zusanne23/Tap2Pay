using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PaySystem.Models
{
    public class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string RFIDUID { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}

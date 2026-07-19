using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Tap2PayAdmin.Models
{
    public class TopUpHistory
    {
        public int TopUpId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string RFIDUID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TopUpDate { get; set; }
    }
}

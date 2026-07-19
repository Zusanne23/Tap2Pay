using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class TopUp
    {
        public int TopUpId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TopUpDate { get; set; }
    }
}
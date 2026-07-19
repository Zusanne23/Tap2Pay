using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap2PayAdmin.Models
{
    public class ReportSummary
    {
        public int TotalUsers { get; set; }

        public int TotalTransactions { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalTopUp { get; set; }
    }
}
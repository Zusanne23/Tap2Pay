using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;

namespace Tap2PayAdmin.Services
{
    public class ReportsService
    {
        private readonly ReportsRepository repository =
            new ReportsRepository();

        public ReportSummary GetSummary()
        {
            return repository.GetSummary();
        }

        public List<Transaction> GetSalesReport()
        {
            return repository.GetSalesReport();
        }
    }
}
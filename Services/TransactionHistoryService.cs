using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Services;
using Tap2PayAdmin.Repositories;
using Microsoft.Data.SqlClient;

namespace Tap2PayAdmin.Services
{
    public class TransactionHistoryService
    {
        private readonly TransactionHistoryRepository repository =
            new TransactionHistoryRepository();

        public List<Transaction> GetAllTransactions()
        {
            return repository.GetAllTransactions();
        }
    }
}
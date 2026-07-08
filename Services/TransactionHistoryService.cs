using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class TransactionHistoryService
    {
        private readonly TransactionHistoryRepository repository =
            new TransactionHistoryRepository();

        public List<TransactionHistory> GetTransactions()
        {
            return repository.GetTransactions();
        }
    }
}
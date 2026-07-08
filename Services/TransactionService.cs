using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PaySystem.Models;
using Tap2PaySystem.Repositories;

namespace Tap2PaySystem.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository transactionRepository =
            new TransactionRepository();

        private readonly TransactionItemRepository transactionItemRepository =
            new TransactionItemRepository();

        public int AddTransaction(Transaction transaction)
        {
            return transactionRepository.AddTransaction(transaction);
        }

        public void AddTransactionItem(TransactionItem item)
        {
            transactionItemRepository.AddTransactionItem(item);
        }
    }
}
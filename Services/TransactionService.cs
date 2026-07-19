using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Models;
using Tap2PayAdmin.Repositories;
using Microsoft.Data.SqlClient;

namespace Tap2PayAdmin.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository repository = new TransactionRepository();
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;
              Database=Tap2PayAdminDb;
              Trusted_Connection=True;
              TrustServerCertificate=True;";

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> list = new List<Transaction>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM Transactions
                                 ORDER BY TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Transaction
                    {
                        TransactionId = Convert.ToInt32(reader["TransactionId"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        RFIDUID = reader["RFIDUID"].ToString(),
                        OrderPurchased = reader["OrderPurchased"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                        Status = reader["Status"].ToString(),
                        PaymentMethod = reader["PaymentMethod"].ToString()
                    });
                }
            }

            return list;
        }

        public int GetTotalTransactions()
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd =
                new SqlCommand("SELECT COUNT(*) FROM Transactions", conn);

            return (int)cmd.ExecuteScalar();
        }

        public decimal GetTotalSales()
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd =
                new SqlCommand("SELECT ISNULL(SUM(TotalAmount),0) FROM Transactions", conn);

            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public decimal GetTodaySales()
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd =
                new SqlCommand(@"
                    SELECT ISNULL(SUM(TotalAmount),0)
                    FROM Transactions
                    WHERE CAST(TransactionDate AS DATE)=CAST(GETDATE() AS DATE)", conn);

            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public int GetCashTransactions()
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd =
                new SqlCommand("SELECT COUNT(*) FROM Transactions WHERE PaymentMethod='Cash'", conn);

            return (int)cmd.ExecuteScalar();
        }

        public int GetRFIDTransactions()
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd =
                new SqlCommand("SELECT COUNT(*) FROM Transactions WHERE PaymentMethod='RFID'", conn);

            return (int)cmd.ExecuteScalar();
        }

        public int AddTransaction(Transaction transaction)
        {
            return repository.AddTransaction(transaction);
        }

        public void AddTransactionItem(TransactionItem item)
        {
            repository.AddTransactionItem(item);
        }
    }
}
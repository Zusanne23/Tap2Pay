using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tap2PayAdmin.Data;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public class TransactionHistoryRepository
    {
        private readonly DbConnection db = new DbConnection();

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> list = new List<Transaction>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                                SELECT
                                    t.TransactionId,
                                    u.FullName AS CustomerName,
                                    u.RFIDUID,
                                    p.ProductName AS OrderPurchased,
                                    ti.Quantity,
                                    t.TotalAmount,
                                    t.TransactionDate,
                                    t.Status
                                FROM Transactions t
                                INNER JOIN Users u
                                    ON t.UserId = u.UserId
                                INNER JOIN TransactionItem ti
                                    ON t.TransactionId = ti.TransactionId
                                INNER JOIN Products p
                                    ON ti.ProductId = p.ProductId
                                ORDER BY t.TransactionDate DESC";

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
                        Status = reader["Status"].ToString()
                    });
                }

            }

            return list;
        }
    }
}
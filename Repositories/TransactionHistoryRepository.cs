using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Tap2PaySystem.Data;
using Tap2PaySystem.Models;

namespace Tap2PaySystem.Repositories
{
    public class TransactionHistoryRepository
    {
        private readonly DbConnection db = new DbConnection();

        public List<TransactionHistory> GetTransactions()
        {
            List<TransactionHistory> list = new List<TransactionHistory>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT
                    t.TransactionId,
                    t.UserId,
                    u.FullName,
                    u.RFIDUID,
                    t.TotalAmount,
                    t.PaymentMethod,
                    t.TransactionDate,
                    t.Status
                FROM Transactions t
                INNER JOIN Users u
                    ON t.UserId = u.UserId
                ORDER BY t.TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new TransactionHistory
                    {
                        TransactionId = (int)reader["TransactionId"],
                        UserId = (int)reader["UserId"],
                        FullName = reader["FullName"].ToString(),
                        RFIDUID = reader["RFIDUID"].ToString(),
                        TotalAmount = (decimal)reader["TotalAmount"],
                        PaymentMethod = reader["PaymentMethod"].ToString(),
                        TransactionDate = (DateTime)reader["TransactionDate"],
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return list;
        }
    }
}
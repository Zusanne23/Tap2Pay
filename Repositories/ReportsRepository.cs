using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Tap2PayAdmin.Data;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public class ReportsRepository
    {
        private readonly DbConnection db = new DbConnection();

        public ReportSummary GetSummary()
        {
            ReportSummary summary = new ReportSummary();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM Users),
                        (SELECT COUNT(*) FROM Transactions),
                        (SELECT ISNULL(SUM(TotalAmount),0) FROM Transactions),
                        (SELECT ISNULL(SUM(Amount),0) FROM TopUp)", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    summary.TotalUsers = reader.GetInt32(0);
                    summary.TotalTransactions = reader.GetInt32(1);
                    summary.TotalSales = reader.GetDecimal(2);
                    summary.TotalTopUp = reader.GetDecimal(3);
                }
            }

            return summary;
        }
        public List<Transaction> GetSalesReport()
        {
            List<Transaction> list = new();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT
                        t.TransactionId,
                        u.FullName,
                        t.TransactionDate,
                        t.TotalAmount,
                        t.PaymentMethod
                    FROM Transactions t
                    INNER JOIN Users u
                        ON t.UserId = u.UserId
                    ORDER BY t.TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Transaction
                    {
                        TransactionId = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        TransactionDate = reader.GetDateTime(2),
                        TotalAmount = reader.GetDecimal(3),
                        PaymentMethod = reader.GetString(4)
                    });
                }
            }

            return list;
        }
    }
}
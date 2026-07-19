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
    public class TransactionRepository
    {
        private readonly DbConnection db = new DbConnection();

        public int AddTransaction(Transaction transaction)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    MessageBox.Show("Connection Opened");

                    string query = @"
                                    INSERT INTO Transactions
                                    (UserId, TotalAmount, PaymentMethod, TransactionDate, Status)
                                    OUTPUT INSERTED.TransactionId
                                    VALUES
                                    (@UserId,@TotalAmount,@PaymentMethod,@TransactionDate,@Status)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@UserId", transaction.UserId);
                    cmd.Parameters.AddWithValue("@TotalAmount", transaction.TotalAmount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", transaction.PaymentMethod);
                    cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                    cmd.Parameters.AddWithValue("@Status", transaction.Status);

                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    MessageBox.Show("Saved Transaction ID = " + id);

                    return id;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                throw;
            }
        }

        public void AddTransactionItem(TransactionItem item)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                                INSERT INTO TransactionItem
                                (TransactionId, ProductId, Quantity, Price, Amount)
                                VALUES
                                (@TransactionId,@ProductId,@Quantity,@Price,@Amount)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TransactionId", item.TransactionId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);

                cmd.ExecuteNonQuery();
                    }
        }
    }
}
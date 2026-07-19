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
    public class TransactionItemRepository
    {
        private readonly DbConnection db = new DbConnection();

        public void AddTransactionItem(TransactionItem item)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                INSERT INTO TransactionItem
                (TransactionId,ProductId,Quantity,Price,Amount)
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

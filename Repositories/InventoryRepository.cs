using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayAdmin.Data;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly Tap2PayAdmin.Data.DbConnection dbConnection =
        new Tap2PayAdmin.Data.DbConnection();
        public List<Inventory> GetAllItems()
        {
            List<Inventory> list = new List<Inventory>();

            using(SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Inventory";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Inventory
                    {
                        InventoryId = (int)reader["InventoryId"],
                        ItemName = reader["ItemName"].ToString(),
                        Price = (decimal)reader["Price"],
                        Stock = (int)reader["Stock"],
                        ExpirationDate = (System.DateTime)reader["ExpirationDate"],
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return list;
        }

        public void AddItem(Inventory item)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Inventory
                                (ItemName,Price,Stock,ExpirationDate,Status)
                                VALUES
                                (@ItemName,@Price,@Stock,@ExpirationDate,@Status)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Stock", item.Stock);
                cmd.Parameters.AddWithValue("@ExpirationDate", item.ExpirationDate);
                cmd.Parameters.AddWithValue("@Status", item.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateItem(Inventory item)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Inventory SET
                                ItemName=@ItemName,
                                Price=@Price,
                                Stock=@Stock,
                                ExpirationDate=@ExpirationDate,
                                Status=@Status
                                WHERE InventoryId=@InventoryId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@InventoryId", item.InventoryId);
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Stock", item.Stock);
                cmd.Parameters.AddWithValue("@ExpirationDate", item.ExpirationDate);
                cmd.Parameters.AddWithValue("@Status", item.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteItem(int inventoryId)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Inventory WHERE InventoryId=@InventoryId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                cmd.ExecuteNonQuery();


            }
        }

        public void DeductStock(int inventoryId, int quantity)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Inventory
                         SET Stock = Stock - @Quantity
                         WHERE InventoryId = @InventoryId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                cmd.ExecuteNonQuery();
            }
        }

        public int GetTotalProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Inventory", conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int GetAvailableProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Inventory WHERE Status='Available'";

                SqlCommand cmd = new SqlCommand(query, conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetLowStockProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Inventory WHERE Stock <= 5 AND Stock > 0";

                SqlCommand cmd = new SqlCommand(query, conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetOutOfStockProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Inventory WHERE Stock <= 0";

                SqlCommand cmd = new SqlCommand(query, conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
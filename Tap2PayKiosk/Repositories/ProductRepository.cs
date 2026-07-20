using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap2PayKiosk.Model;
using Tap2PayKiosk.Data;
using Microsoft.Data.SqlClient;

namespace Tap2PayKiosk.Repositories
{
    public class ProductRepository
    {
        private readonly DbConnection dbConnection = new DbConnection();

        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Products";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = (decimal)reader["Price"],
                        Status = reader["Status"].ToString(),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
            }

            return list;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Products
                                (ProductName, Category, Price, Status, ImagePath)
                                VALUES
                                (@ProductName, @Category, @Price, @Status, @ImagePath)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Status", product.Status);
                cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
            UPDATE Products SET
                ProductName = @ProductName,
                Category = @Category,
                Price = @Price,
                Status = @Status,
                ImagePath = @ImagePath
            WHERE ProductId = @ProductId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Status", product.Status);
                cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Products WHERE ProductId=@ProductId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductId", productId);

                cmd.ExecuteNonQuery();
            }
        }

        public int GetTotalProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetAvailableProducts()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Status='Available'", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetMealsCount()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Category='Meals'", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetDrinksCount()
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Category='Drinks'", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
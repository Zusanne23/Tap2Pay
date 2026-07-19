using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Tap2PayAdmin.Data;
using Tap2PayAdmin.Models;

namespace Tap2PayAdmin.Repositories
{
    public class TopUpRepository : ITopUpRepository
    {
        private readonly DbConnection db = new DbConnection();

        public void AddTopUp(TopUp topUp)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO TopUp(UserId, Amount, TopUpDate)
                                 VALUES(@UserId,@Amount,@TopUpDate)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UserId", topUp.UserId);
                cmd.Parameters.AddWithValue("@Amount", topUp.Amount);
                cmd.Parameters.AddWithValue("@TopUpDate", topUp.TopUpDate);

                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Users";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        RFIDUID = reader["RFIDUID"].ToString(),
                        Role = reader["Role"].ToString(),
                        Status = reader["Status"].ToString(),
                        Balance = Convert.ToDecimal(reader["Balance"])
                    });
                }
            }

            return users;
        }

        public void UpdateBalance(int userId, decimal newBalance)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Users
                                 SET Balance=@Balance
                                 WHERE UserId=@UserId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Balance", newBalance);
                cmd.Parameters.AddWithValue("@UserId", userId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
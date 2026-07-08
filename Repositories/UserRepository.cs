using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using Tap2PaySystem.Data;
using Tap2PaySystem.Models;

namespace Tap2PaySystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection dbConnection = new DbConnection();

        public User Login(string username, string password)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                MessageBox.Show(conn.Database);

                string query = @"SELECT * FROM Users
                                 WHERE Username = @Username
                                 AND Password = @Password
                                 AND Status = 'Active'";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No matching user found.");
                }

                if (reader.Read())
                {
                    return new User
                    {
                        UserId = (int)reader["UserId"],
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        RFIDUID = reader["RFIDUID"]?.ToString(),
                        Role = reader["Role"].ToString(),
                        Status = reader["Status"].ToString(),
                        Balance = (decimal)reader["Balance"]
                    };
                }

                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Users";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserId = (int)reader["UserId"],
                        FullName = reader["FullName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        RFIDUID = reader["RFIDUID"]?.ToString(),
                        Role = reader["Role"].ToString(),
                        Status = reader["Status"].ToString(),
                        Balance = (decimal)reader["Balance"]
                    });
                }
            }

            return users;
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Users
                                (FullName, Username, Password, RFIDUID, Role, Status, Balance)
                                VALUES
                                (@FullName, @Username, @Password, @RFIDUID, @Role, @Status, @Balance)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RFIDUID", user.RFIDUID ?? "");
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@Status", user.Status);
                cmd.Parameters.AddWithValue("@Balance", user.Balance);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateBalance(int userId, decimal newBalance)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Users
                         SET Balance = @Balance
                         WHERE UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Balance", newBalance);
                cmd.Parameters.AddWithValue("@UserId", userId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
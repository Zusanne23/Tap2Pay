using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Tap2PayKiosk.Data;
using Tap2PayKiosk.Models;

namespace Tap2PayKiosk.Repositories
{
    internal class UserRepository
    {
        private readonly DbConnection dbConnection = new DbConnection();
        public User GetByRFID(string rfid)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();

                string query = @"SELECT * FROM Users
                         WHERE RFIDUID=@RFID
                         AND Status='Active'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RFID", rfid);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FullName = reader["FullName"].ToString(),
                        RFIDUID = reader["RFIDUID"].ToString(),
                        Balance = Convert.ToDecimal(reader["Balance"])
                    };
                }
            }

            return null;
        }

        public void UpdateBalance(int userId, decimal newBalance)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
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

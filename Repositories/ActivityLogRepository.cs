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
    public class ActivityLogRepository
    {
        private readonly DbConnection db = new DbConnection();

        public void AddLog(ActivityLog log)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                INSERT INTO ActivityLogs
                (
                    UserId,
                    FullName,
                    Role,
                    Action,
                    Details,
                    LogDate
                )
                VALUES
                (
                    @UserId,
                    @FullName,
                    @Role,
                    @Action,
                    @Details,
                    @LogDate
                )";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UserId", log.UserId);
                cmd.Parameters.AddWithValue("@FullName", log.FullName);
                cmd.Parameters.AddWithValue("@Role", log.Role);
                cmd.Parameters.AddWithValue("@Action", log.Action);
                cmd.Parameters.AddWithValue("@Details", log.Details);
                cmd.Parameters.AddWithValue("@LogDate", log.LogDate);

                cmd.ExecuteNonQuery();
            }
        }

        public List<ActivityLog> GetAllLogs()
        {
            List<ActivityLog> logs = new List<ActivityLog>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT *
                FROM ActivityLogs
                ORDER BY LogDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    logs.Add(new ActivityLog
                    {
                        LogId = Convert.ToInt32(reader["LogId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FullName = reader["FullName"].ToString(),
                        Role = reader["Role"].ToString(),
                        Action = reader["Action"].ToString(),
                        Details = reader["Details"].ToString(),
                        LogDate = Convert.ToDateTime(reader["LogDate"])
                    });
                }
            }

            return logs;
        }

        public int GetTotalLogs()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                SqlCommand cmd =
                    new SqlCommand("SELECT COUNT(*) FROM ActivityLogs", conn);

                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tap2PayAdmin.Data;
using Tap2PayAdmin.Models;
using Microsoft.Data.SqlClient;

namespace Tap2PayAdmin.Repositories
{
    public class TopUpHistoryRepository
    {
        private readonly DbConnection db = new DbConnection();

        public List<TopUpHistory> GetTopUpHistory()
        {
            List<TopUpHistory> list = new List<TopUpHistory>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT
                    t.TopUpId,
                    t.UserId,
                    u.FullName,
                    u.RFIDUID,
                    t.Amount,
                    t.TopUpDate
                FROM TopUp t
                INNER JOIN Users u
                    ON t.UserId = u.UserId
                ORDER BY t.TopUpDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        list.Add(new TopUpHistory
                        {
                            TopUpId = Convert.ToInt32(reader["TopUpId"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FullName = reader["FullName"]?.ToString() ?? "",
                            RFIDUID = reader["RFIDUID"]?.ToString() ?? "",
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            TopUpDate = Convert.ToDateTime(reader["TopUpDate"])
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }
            }

            return list;
        }
    }
}
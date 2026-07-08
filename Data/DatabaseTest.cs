using System;
using Microsoft.Data.SqlClient;

namespace Tap2PaySystem.Data
{
    public class DatabaseTest
    {
        public static bool TestConnection()
        {
            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
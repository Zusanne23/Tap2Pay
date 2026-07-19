using Microsoft.Data.SqlClient;

namespace Tap2PayAdmin.Data
{
    public class DbConnection
    {
        private readonly string connectionString =
 @"Server=(localdb)\MSSQLLocalDB;
Initial Catalog=Tap2PaySystemDb;
Integrated Security=True;
TrustServerCertificate=True;";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
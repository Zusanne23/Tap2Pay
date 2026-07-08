using Microsoft.Data.SqlClient;

namespace Tap2PaySystem.Data
{
    public class DbConnection
    {
        private readonly string connectionString =
     @"Server=(localdb)\MSSQLLocalDB;Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tap2PaySystemDb;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
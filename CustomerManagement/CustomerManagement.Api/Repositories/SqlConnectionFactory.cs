using System.Configuration;
using System.Data.SqlClient;

namespace CustomerManagement.Api.Repositories
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerManagementDb"].ConnectionString);
        }
    }
}
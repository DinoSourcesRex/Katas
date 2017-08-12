using System.Data.SqlClient;

namespace CustomerManagement.Api.Repositories
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetConnection();
    }
}
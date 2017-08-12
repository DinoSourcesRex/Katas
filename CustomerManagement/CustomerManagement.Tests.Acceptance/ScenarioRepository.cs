using System.Data.SqlClient;
using Dapper;

namespace CustomerManagement.Tests.Acceptance
{
    public class ScenarioRepository
    {
        public string ConnectionString { get; }
        public string DbName { get; }

        public ScenarioRepository(string connectionString, string dbName)
        {
            ConnectionString = $"Server={connectionString};Integrated Security=True";
            DbName = dbName;
        }

        public void DropDatabase()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "USE MASTER\n" +
                    $"ALTER DATABASE [{DbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n " +
                    $"DROP DATABASE [{DbName}]";

                connection.Execute(sql);
            }
        }
    }
}
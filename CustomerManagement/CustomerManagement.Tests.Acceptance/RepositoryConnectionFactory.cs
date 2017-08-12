using System.Data.SqlClient;
using CustomerManagement.Api.Repositories;

namespace CustomerManagement.Tests.Acceptance
{
    public class RepositoryConnectionFactory : ISqlConnectionFactory
    {
        private string DbName { get; }

        public RepositoryConnectionFactory(string dbName)
        {
            DbName = dbName;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection($"Server=(localdb)\\mssqllocaldb;Initial Catalog={DbName};Integrated Security=True");
        }
    }
}
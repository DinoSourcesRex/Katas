using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CustomerManagement.Tests.Acceptance
{
    public class ScenarioRepository
    {
        public string ServerConnectionString { get; }
        public string DbConnectionString { get; }
        public string DbName { get; }

        public ScenarioRepository(string server, string dbName)
        {
            ServerConnectionString = $"Server={server};Integrated Security=True";
            DbConnectionString = $"Server={server}; Initial Catalog = {dbName}; Integrated Security = True";
            DbName = dbName;
        }

        public void DropDatabase()
        {
            using (var connection = new SqlConnection(ServerConnectionString))
            {
                string sql = "USE MASTER\n" +
                             $"ALTER DATABASE [{DbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE\n " +
                             $"DROP DATABASE [{DbName}]";

                connection.Execute(sql);
            }
        }

        public async void ClearTables()
        {
            using (var connection = new SqlConnection(ServerConnectionString))
            {
                await connection.ExecuteAsync(
                    $"truncate table [{DbName}].[dbo].[CustomerColour]" +
                        $"delete from [{DbName}].[dbo].[Customer]" + 
                        $"DBCC CHECKIDENT ('[{DbName}].[dbo].[Customer]', RESEED, 0)" + 
                        $"delete from [{DbName}].[dbo].[Colour]" + 
                        $"DBCC CHECKIDENT ('[{DbName}].[dbo].[Colour]', RESEED, 0)",
                    commandType: CommandType.Text).ConfigureAwait(false);
            }
        }
    }
}
using System;
using System.Data.SqlClient;
using CustomerManagement.Api.Repositories;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace CustomerManagement.Tests.Acceptance
{
    [Binding]
    public class CustomerManagementBase : ScenarioBase
    {
        protected GetCustomerResultContext GetCustomerResultContext;

        public CustomerManagementBase(GetCustomerResultContext getCustomerResultContext)
        {
            GetCustomerResultContext = getCustomerResultContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            var sqlConnectionFactory = Mock<ISqlConnectionFactory>();
            sqlConnectionFactory.Stub(conn => conn.GetConnection())
                .Do(new Func<SqlConnection>(() => new SqlConnection()
                {
                    ConnectionString = DatabaseSetup.ScenarioRepository.DbConnectionString
                }));
        }
    }
}
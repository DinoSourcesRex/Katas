using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Repositories
{
    public class CustomerManagementRepository : ICustomerManagementRepository
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public CustomerManagementRepository(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task<IReadOnlyCollection<Customer>> GetLatestCustomersAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpsertCustomerAsync(UpsertCustomer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
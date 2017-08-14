using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Client.Models;
using Dapper;
using Customer = CustomerManagement.Api.Repositories.Dto.Customer;

namespace CustomerManagement.Api.Repositories
{
    public class CustomerManagementRepository : ICustomerManagementRepository
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public CustomerManagementRepository(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IReadOnlyCollection<Client.Models.Customer>> GetLatestCustomersAsync()
        {
            using (var connection = _sqlConnection.GetConnection())
            {
                var result = (await connection.QueryAsync<Customer>("dbo.Customer_GetLatest",
                        commandType: CommandType.StoredProcedure, commandTimeout: 0)
                    .ConfigureAwait(false)).ToList().AsReadOnly();

                //Could bring auto mapper in here
                List<Client.Models.Customer> customerList = new List<Client.Models.Customer>();
                foreach (var customer in result)
                {
                    customerList.Add(new Client.Models.Customer
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        PreviouslyOrdered = customer.PreviouslyOrdered,
                        WebCustomer = customer.WebCustomer,
                        LastActive = customer.LastActive,
                        FavouriteColours = new List<string>(customer.FavouriteColours.Split(',').ToList())
                    });
                }

                return customerList.AsReadOnly();
            }
        }

        public async Task<bool> UpsertCustomerAsync(UpsertCustomer customer)
        {
            using (var connection = _sqlConnection.GetConnection())
            {
                var parameters = new
                {
                    Id = customer.Id ?? -1,
                    customer.Name,
                    customer.PreviouslyOrdered,
                    customer.WebCustomer,
                    customer.LastActive,
                    Colours = customer.FavouriteColours.StringToDataTable()
                };

                var affectedRows = await connection.ExecuteAsync("dbo.Customer_Upsert", parameters,
                        commandType: CommandType.StoredProcedure, commandTimeout: 0)
                    .ConfigureAwait(false);

                return affectedRows != 0;
            }
        }
    }
}
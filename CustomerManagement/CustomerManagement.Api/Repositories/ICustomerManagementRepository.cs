using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Repositories
{
    public interface ICustomerManagementRepository
    {
        Task<IReadOnlyCollection<Customer>> GetLatestCustomersAsync();
        Task<bool> UpsertCustomerAsync(UpsertCustomer customer);
    }
}
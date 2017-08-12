using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Api.Models;

namespace VendingMachine.Api.Repositories
{
    public interface IVendingMachineRepository
    {
        Task<List<Product>> GetProducts();
    }
}
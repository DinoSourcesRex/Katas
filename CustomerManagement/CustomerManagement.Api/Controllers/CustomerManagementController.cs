using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerManagement.Api.Models;
using CustomerManagement.Api.Repositories;

namespace CustomerManagement.Api.Controllers
{
    public class CustomerManagementController : ApiController
    {
        private readonly ICustomerManagementRepository _repository;

        public CustomerManagementController(ICustomerManagementRepository repository)
        {
            _repository = repository;
        }

        [Route("customer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync()
        {
            var customerList = await _repository.GetLatestCustomersAsync();

            foreach (var customer in customerList)
            {
                customer.Palindrome = customer.FirstName.ToLower().SequenceEqual(customer.FirstName.ToLower().Reverse());
            }

            return Ok(customerList);
        }

        [Route("customer")]
        [HttpPost]
        public async Task<IHttpActionResult> UpsertAsync(UpsertCustomer customer)
        {
            return Ok(await _repository.UpsertCustomerAsync(customer));
        }
    }
}
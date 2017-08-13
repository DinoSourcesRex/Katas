using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerManagement.Client.Models;

namespace CustomerManagement.Client
{
    public class CustomerManagementApiClient
    {
        private readonly HttpClient _httpClient;

        public CustomerManagementApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Customer>> GetCustomerAsync()
        {
            var response = await _httpClient.GetAsync("customer");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IReadOnlyCollection<Customer>>();
        }

        public async Task<bool> UpsertCustomerAsync(UpsertCustomer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("customer", customer);
            return response.IsSuccessStatusCode;
        }
    }
}
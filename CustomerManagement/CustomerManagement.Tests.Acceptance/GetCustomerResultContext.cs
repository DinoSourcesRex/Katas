using System.Collections.Generic;

namespace CustomerManagement.Tests.Acceptance
{
    public class GetCustomerResultContext
    {
        public GetCustomerResultContext()
        {
            Results = null;
        }

        public IReadOnlyCollection<Client.Models.Customer> Results;
    }
}
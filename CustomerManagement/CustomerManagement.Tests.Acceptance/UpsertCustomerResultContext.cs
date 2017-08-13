using CustomerManagement.Client.Models;

namespace CustomerManagement.Tests.Acceptance
{
    public class UpsertCustomerResultContext
    {
        public UpsertCustomerResultContext()
        {
            AddSuccessful = false;
            NewCustomer = null;
        }

        public bool AddSuccessful;
        public UpsertCustomer NewCustomer;
    }
}
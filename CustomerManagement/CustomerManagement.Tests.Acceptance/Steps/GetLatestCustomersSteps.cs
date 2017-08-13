using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Client.Models;
using CustomerManagement.Tests.Acceptance.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CustomerManagement.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class GetLatestCustomersSteps : CustomerManagementBase
    {
        public GetLatestCustomersSteps(GetCustomerResultContext getCustomerResultContext) : base(getCustomerResultContext)
        {
        }

        [Given(@"the following records in the database")]
        public void GivenTheFollowingRecordsInTheDatabase(Table table)
        {
            DatabaseSetup.ScenarioRepository.ClearTables();

            var customersTable = table.CreateSet<CustomerRow>();

            foreach (var customer in customersTable)
            {
                //Could have used automapper here but it would be overkill for just the 1 mapping
                ApiClient.UpsertCustomerAsync(new UpsertCustomer
                {
                    Name = customer.Name,
                    PreviouslyOrdered = customer.PreviouslyOrdered,
                    WebCustomer = customer.WebCustomer,
                    LastActive = customer.LastActive,
                    FavouriteColours = new List<string>(customer.FavouriteColours.Split(',').ToList())
                }).Wait();
            }
        }

        [When(@"I make a request to get latest customers")]
        public void WhenIMakeARequestToGetLatestCustomers()
        {
            GetCustomerResultContext.Results = ApiClient.GetCustomerAsync().Result;
        }

        [Then(@"I should receive (.*) results")]
        public void ThenIShouldReceiveResults(int p0)
        {
            Assert.AreEqual(20, GetCustomerResultContext.Results.Count);
        }

        [Then(@"the results should be ordered by the most recent customer")]
        public void ThenTheResultsShouldBeOrderedByTheMostRecentCustomer()
        {
            var copiedResults = new List<Client.Models.Customer>(GetCustomerResultContext.Results);

            copiedResults = copiedResults.OrderByDescending(c => c.LastActive).ToList();

            CollectionAssert.AreEqual(copiedResults, GetCustomerResultContext.Results);
        }

        [Then(@"none of the records should have null properties")]
        public void ThenNoneOfTheRecordsShouldHaveNullProperties()
        {
            foreach (var customer in GetCustomerResultContext.Results)
            {
                Assert.NotNull(customer.Id);
                Assert.NotNull(customer.Name);
                Assert.NotNull(customer.PreviouslyOrdered);
                Assert.NotNull(customer.WebCustomer);
                Assert.NotNull(customer.LastActive);
                Assert.NotNull(customer.FavouriteColours);
                Assert.NotNull(customer.Palindrome);
            }
        }
    }
}
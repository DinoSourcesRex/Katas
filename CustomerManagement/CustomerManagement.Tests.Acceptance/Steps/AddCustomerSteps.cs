using System;
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
    public sealed class AddCustomerSteps : CustomerManagementBase
    {
        private readonly UpsertCustomerResultContext _upsertResultContext;

        public AddCustomerSteps(GetCustomerResultContext getCustomerResultContext, UpsertCustomerResultContext upsertResultContext) : base(getCustomerResultContext)
        {
            _upsertResultContext = upsertResultContext;
        }

        [Given(@"the following new customer")]
        [Given(@"the following edits to an existing customer")]
        public void GivenTheFollowingNewCustomer(Table table)
        {
            var customer = table.CreateSet<CustomerRow>().FirstOrDefault();

            if (customer == null)
            {
                throw new ArgumentNullException("New customer is null.");
            }

            _upsertResultContext.NewCustomer = new UpsertCustomer
            {
                Id = customer.Id,
                Name = customer.Name,
                PreviouslyOrdered = customer.PreviouslyOrdered,
                WebCustomer = customer.WebCustomer,
                LastActive = customer.LastActive,
                FavouriteColours = new List<string>(customer.FavouriteColours.Split(',').ToList())
            };
        }

        [When(@"I add add new customer")]
        [When(@"I edit an existing customer")]
        public void WhenIAddAddNewCustomer()
        {
            _upsertResultContext.AddSuccessful = ApiClient.UpsertCustomerAsync(_upsertResultContext.NewCustomer).Result;
        }

        [Then(@"I should see the new customer in the results")]
        public void ThenIShouldSeeTheNewCustomerInTheResults()
        {
            Assert.IsTrue(GetCustomerResultContext.Results.Any(r => 
                r.Name == _upsertResultContext.NewCustomer.Name && 
                r.PreviouslyOrdered == _upsertResultContext.NewCustomer.PreviouslyOrdered &&
                r.WebCustomer == _upsertResultContext.NewCustomer.WebCustomer && 
                r.LastActive == _upsertResultContext.NewCustomer.LastActive &&
                _upsertResultContext.NewCustomer.FavouriteColours.All(item => r.FavouriteColours.Contains(item))));
        }

        [Then(@"I should recieve a successful response")]
        public void ThenIShouldRecieveASuccessfulResponse()
        {
            Assert.IsTrue(_upsertResultContext.AddSuccessful);
        }
    }
}
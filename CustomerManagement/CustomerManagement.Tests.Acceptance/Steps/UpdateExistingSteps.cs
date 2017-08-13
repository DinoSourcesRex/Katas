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
    public sealed class UpdateExistingSteps : CustomerManagementBase
    {
        private readonly UpsertCustomerResultContext _upsertResultContext;

        public UpdateExistingSteps(GetCustomerResultContext getCustomerResultContext, UpsertCustomerResultContext upsertResultContext) : base(getCustomerResultContext)
        {
            _upsertResultContext = upsertResultContext;
        }

        [Then(@"I should see the changes to the customer should have propogated")]
        public void ThenIShouldSeeTheChangesToTheCustomerShouldHavePropogated()
        {
            var updatedRecord = GetCustomerResultContext.Results.Single(r => r.Id == _upsertResultContext.NewCustomer.Id);

            Assert.Multiple(() =>
                {
                    Assert.IsNotNull(updatedRecord);
                    Assert.AreEqual(_upsertResultContext.NewCustomer.Name, updatedRecord.Name);
                    Assert.AreEqual(_upsertResultContext.NewCustomer.PreviouslyOrdered, updatedRecord.PreviouslyOrdered);
                    Assert.AreEqual(_upsertResultContext.NewCustomer.WebCustomer, updatedRecord.WebCustomer);
                    Assert.AreEqual(_upsertResultContext.NewCustomer.LastActive, updatedRecord.LastActive);
                    CollectionAssert.AreEquivalent(_upsertResultContext.NewCustomer.FavouriteColours, updatedRecord.FavouriteColours);
                });
        }
    }
}
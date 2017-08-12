using System;
using System.Threading;
using Rhino.Mocks;
using TechTalk.SpecFlow;
using VendingMachine.Api.Models;
using VendingMachine.Tests.Acceptance.StepData;

namespace VendingMachine.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class PurchaseProductSteps
    {
        private readonly VendingMachineData _vendingMachineData;

        public PurchaseProductSteps(VendingMachineData vendingMachineData)
        {
            _vendingMachineData = vendingMachineData;
        }

        [Then(@"the vending machine should give me my (.*)")]
        public void ThenTheVendingMachineShouldGiveMeMyItem(string product)
        {
            _vendingMachineData.VendingMachineHardware.AssertWasCalled(h => h.EjectItem(Arg<Product>.Matches(p => p.Name.Equals(product, StringComparison.OrdinalIgnoreCase))),
                options => options.Repeat.Once());
        }

        [Then(@"I wait (.*) milliseconds")]
        public void ThenIWaitMillisconds(int waitTime)
        {
            Thread.Sleep(waitTime);
        }
    }
}
using NUnit.Framework;
using TechTalk.SpecFlow;
using VendingMachine.Tests.Acceptance.StepData;

namespace VendingMachine.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class VendingMachineDisplaySteps
    {
        private readonly VendingMachineData _vendingMachineData;

        public VendingMachineDisplaySteps(VendingMachineData vendingMachineData)
        {
            _vendingMachineData = vendingMachineData;
        }

        [Then(@"the vending machine should display (.*)")]
        public void ThenTheVendingMachineShouldDisplayTheMessage(string message)
        {
            Assert.AreEqual(message, _vendingMachineData.VendingMachineDisplay.Message);
        }
    }
}
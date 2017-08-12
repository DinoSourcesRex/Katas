using TechTalk.SpecFlow;
using VendingMachine.Tests.Acceptance.StepData;

namespace VendingMachine.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class CancelPurchaseSteps
    {
        private readonly VendingMachineData _vendingMachineData;

        public CancelPurchaseSteps(VendingMachineData vendingMachineData)
        {
            _vendingMachineData = vendingMachineData;
        }

        [When(@"I cancel my purchase")]
        public void WhenICancelMyPurchase()
        {
            _vendingMachineData.VendingMachine.CancelTransaction();
        }
    }
}
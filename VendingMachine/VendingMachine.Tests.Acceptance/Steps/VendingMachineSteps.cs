using System.Collections.Generic;
using Rhino.Mocks;
using TechTalk.SpecFlow;
using VendingMachine.Api.Controllers;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Models;
using VendingMachine.Api.Repositories;
using VendingMachine.Tests.Acceptance.StepData;

namespace VendingMachine.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class VendingMachineSteps
    {
        private readonly VendingMachineData _vendingMachineData;

        public VendingMachineSteps(VendingMachineData vendingMachineData)
        {
            _vendingMachineData = vendingMachineData;
        }

        [BeforeScenario]
        public void Setup()
        {
            _vendingMachineData.ChangeCalculator = new ChangeCalculator();
            _vendingMachineData.VendingMachineRepository = MockRepository.GenerateMock<IVendingMachineRepository>();
            _vendingMachineData.PurchaseHandler = new PurchaseHandler(_vendingMachineData.ChangeCalculator, _vendingMachineData.VendingMachineRepository);

            _vendingMachineData.VendingMachineDisplay = new VendingMachineDisplay();
            _vendingMachineData.VendingMachineHardware = MockRepository.GenerateMock<IVendingMachineHardware>();
            _vendingMachineData.VendingMachine = new VendingMachineController(_vendingMachineData.PurchaseHandler, _vendingMachineData.VendingMachineDisplay, _vendingMachineData.VendingMachineHardware);
        }

        [Given(@"that a (.*) costs a total of £(.*)")]
        public void GivenThatAProductCostsATotalOfProductCost(string productName, double productCost)
        {
            _vendingMachineData.VendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(new List<Product>
                {
                    new Product
                    {
                        Name = productName,
                        Cost = productCost
                    }
                });
        }

        [When(@"I enter £(.*) into the machine")]
        public void WhenIEnterACoinIntoTheMachine(double coin)
        {
            _vendingMachineData.VendingMachine.InsertCoinAsync(coin).GetAwaiter().GetResult();
        }

        [When(@"I select (.*)")]
        public void WhenISelectAProduct(string productName)
        {
            _vendingMachineData.VendingMachine.SelectProductAsync(new Product
            {
                Name = productName
            }).GetAwaiter().GetResult();
        }


        [Then(@"there should be £(.*) change in the coin tray")]
        public void ThenThereShouldBeChangeInTheCoinTray(double totalChange)
        {
            _vendingMachineData.VendingMachineHardware.AssertWasCalled(h => h.EjectChange(totalChange), options => options.Repeat.Once());
        }
    }
}
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using VendingMachine.Api.Controllers;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Models;

namespace VendingMachine.Tests.Controllers
{
    public class VendingMachineControllerTests
    {
        private IPurchaseHandler _mockPurchaseHandler;
        private IVendingMachineDisplay _mockVendingMachineDisplay;
        private IVendingMachineHardware _mockVendingMachineHardware;

        private VendingMachineController _sut;

        [SetUp]
        public void Setup()
        {
            _mockPurchaseHandler = MockRepository.GenerateMock<IPurchaseHandler>();
            _mockVendingMachineDisplay = MockRepository.GenerateMock<IVendingMachineDisplay>();
            _mockVendingMachineHardware = MockRepository.GenerateMock<IVendingMachineHardware>();

            _sut = new VendingMachineController(_mockPurchaseHandler, _mockVendingMachineDisplay, _mockVendingMachineHardware);
        }

        [Test]
        public async Task SelectProduct_CompletedTransaction_ExpectEjectedItem()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            _mockPurchaseHandler.Stub(pH => pH.SelectProductAsync(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .ReturnAsync(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            await _sut.SelectProductAsync(product);

            _mockVendingMachineHardware.AssertWasCalled(h => h.EjectItem(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))),
                options => options.Repeat.Once());
        }

        [Test]
        public async Task SelectProduct_CompletedTransaction_ExpectEjectChange()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double changeDue = 3;

            _mockPurchaseHandler.Stub(pH => pH.SelectProductAsync(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .ReturnAsync(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = changeDue
                });

            await _sut.SelectProductAsync(product);

            _mockVendingMachineHardware.AssertWasCalled(h => h.EjectChange(changeDue), options => options.Repeat.Once());
        }

        [Test]
        public async Task SelectProduct_CompletedTransaction_ExpectPrintGratitude()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            _mockPurchaseHandler.Stub(pH => pH.SelectProductAsync(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .ReturnAsync(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            await _sut.SelectProductAsync(product);

            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintGratitude(), options => options.Repeat.Once());
        }

        [Test]
        public async Task SelectProduct_CompletedTransaction_ExpectPrintInsertCoin()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            _mockPurchaseHandler.Stub(pH => pH.SelectProductAsync(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .ReturnAsync(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            await _sut.SelectProductAsync(product);

            //We are waiting for the machine to reset.
            await Task.Delay(1000);

            //This gets called after a delay.
            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintInsertCoin(), options => options.Repeat.Once());
        }

        [Test]
        public async Task SelectProduct_PendingTransaction_ExpectPrintPaymentDue()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double paymentDue = 3;

            _mockPurchaseHandler.Stub(pH => pH.SelectProductAsync(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .ReturnAsync(new PurchaseResult
                {
                    Accepted = false,
                    AmountDue = paymentDue
                });

            await _sut.SelectProductAsync(product);

            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintPaymentDue(paymentDue), options => options.Repeat.Once());
        }

        [Test]
        public async Task InsertCoin_CompletedTransaction_ExpectEjectedItem()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double coinInserted = 8;

            _mockPurchaseHandler.Stub(pH => pH.InsertCoin(coinInserted))
                .Return(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            _mockPurchaseHandler.Stub(pH => pH.SelectedProduct)
                .Return(product);

            await _sut.InsertCoinAsync(coinInserted);

            _mockVendingMachineHardware.AssertWasCalled(h => h.EjectItem(Arg<Product>.Matches(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))),
                options => options.Repeat.Once());
        }

        [Test]
        public async Task InsertCoin_CompletedTransaction_EjectChange()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double coinInserted = 8;
            double changeDue = 3;

            _mockPurchaseHandler.Stub(pH => pH.InsertCoin(coinInserted))
                .Return(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = changeDue
                });

            _mockPurchaseHandler.Stub(pH => pH.SelectedProduct)
                .Return(product);

            await _sut.InsertCoinAsync(coinInserted);

            _mockVendingMachineHardware.AssertWasCalled(h => h.EjectChange(changeDue), options => options.Repeat.Once());
        }

        [Test]
        public async Task InsertCoin_CompletedTransaction_ExpectPrintGratitude()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double coinInserted = 8;

            _mockPurchaseHandler.Stub(pH => pH.InsertCoin(coinInserted))
                .Return(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            _mockPurchaseHandler.Stub(pH => pH.SelectedProduct)
                .Return(product);

            await _sut.InsertCoinAsync(coinInserted);

            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintGratitude(), options => options.Repeat.Once());
        }

        [Test]
        public async Task InsertCoin_CompletedTransaction_ExpectPrintInsertCoin()
        {
            var product = new Product
            {
                Name = "Item",
                Cost = 5
            };

            double coinInserted = 8;

            _mockPurchaseHandler.Stub(pH => pH.InsertCoin(coinInserted))
                .Return(new PurchaseResult
                {
                    Accepted = true,
                    AmountDue = 3
                });

            _mockPurchaseHandler.Stub(pH => pH.SelectedProduct)
                .Return(product);

            //This has an 800ms internal delay.
            await _sut.InsertCoinAsync(coinInserted);

            //This gets called after a delay. Couldn't think of a better way to do this.
            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintInsertCoin(), options => options.Repeat.Once());
        }

        [Test]
        public async Task InsertCoin_NullResult_ExpectNoThrow()
        {
            double coinInserted = 8;

            _mockPurchaseHandler.Stub(pH => pH.InsertCoin(coinInserted))
                .Return(null);

            Assert.DoesNotThrowAsync(async () => await _sut.InsertCoinAsync(coinInserted));
        }

        [Test]
        public void CancelTransaction_ExpectEjectChange()
        {
            double changeDue = 8;

            _mockPurchaseHandler.Stub(pH => pH.CancelPurchase())
                .Return(changeDue);

            _sut.CancelTransaction();

            _mockVendingMachineHardware.AssertWasCalled(h => h.EjectChange(changeDue), options => options.Repeat.Once());
        }

        [Test]
        public void CancelTransaction_ExpectPrintInsertCoin()
        {
            _sut.CancelTransaction();

            _mockVendingMachineDisplay.AssertWasCalled(d => d.PrintInsertCoin(), options => options.Repeat.Once());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rhino.Mocks;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Models;
using VendingMachine.Api.Repositories;

namespace VendingMachine.Tests.Infrastructure
{
    public class PurchaseHandlerTests
    {
        private IChangeCalculator _mockChangeCalculator;
        private IVendingMachineRepository _mockVendingMachineRepository;

        private Fixture _fixture;

        private PurchaseHandler _sut;

        [SetUp]
        public void Setup()
        {
            _mockChangeCalculator = MockRepository.GenerateMock<IChangeCalculator>();
            _mockVendingMachineRepository = MockRepository.GenerateMock<IVendingMachineRepository>();

            _fixture = new Fixture();
            _sut = new PurchaseHandler(_mockChangeCalculator, _mockVendingMachineRepository);
        }

        [Test]
        public void InsertCoin_ExpectCoinAdded()
        {
            Assert.AreEqual(0, _sut.CoinsEntered.Count);

            _sut.InsertCoin(1);
            _sut.InsertCoin(2);
            _sut.InsertCoin(3);
            _sut.InsertCoin(4);

            Assert.AreEqual(4, _sut.CoinsEntered.Count);
        }

        [Test]
        public void InsertCoin_ExpectTransactionInProgress()
        {
            Assert.AreEqual(TransactionState.StandBy, _sut.TransactionState);

            _sut.InsertCoin(5);

            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);
        }

        [Test]
        public async Task InsertCoin_PurchaseSuccessful_ExpectTransactionStandBy()
        {
            Assert.AreEqual(TransactionState.StandBy, _sut.TransactionState);

            double productCost = 2;
            double insertedCoin = 2;

            var product = new Product
            {
                Name = "Snickers",
                Cost = productCost
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(PopulateProducts(product));

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(-productCost).Repeat.Once();

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(insertedCoin - productCost);

            await _sut.SelectProductAsync(product);

            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);

            var result = _sut.InsertCoin(insertedCoin).Accepted;

            Assert.AreEqual(true, result);
            Assert.AreEqual(TransactionState.StandBy, _sut.TransactionState);
        }

        [Test]
        public async Task InsertCoint_PurchaseUnsuccessful_ExpectTransactionInProgress()
        {
            Assert.AreEqual(TransactionState.StandBy, _sut.TransactionState);

            double productCost = 2;
            double insertedCoin = 1;

            var product = new Product
            {
                Name = "Snickers",
                Cost = productCost
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(PopulateProducts(product));

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(-productCost).Repeat.Once();

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(insertedCoin - productCost);

            await _sut.SelectProductAsync(product);

            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);

            var result = _sut.InsertCoin(insertedCoin);

            Assert.AreEqual(false, result.Accepted);
            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);
        }

        [TestCase(2, 0, 2)]
        [TestCase(2, 1, 3)]
        public async Task InsertCoin_PurchaseSuccessful_ExpectCorrectChange(double productCost, double expectedChange, double insertedCoin)
        {
            var product = new Product
            {
                Name = "Snickers",
                Cost = productCost
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(PopulateProducts(product));

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(
                    t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase) &&
                         t.Product.Cost == productCost &&
                         t.CoinsEntered.FirstOrDefault() == insertedCoin)))
                .Return(insertedCoin - productCost);

            await _sut.SelectProductAsync(product);

            var result = _sut.InsertCoin(insertedCoin);

            Assert.AreEqual(true, result.Accepted);
            Assert.AreEqual(expectedChange, result.AmountDue);
        }

        [Test]
        public async Task SelectProduct_NoValidProduct_ExpectException()
        {
            var product = new Product
            {
                Name = "Doesn't Exist",
                Cost = 9000.1
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(new List<Product>());

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _sut.SelectProductAsync(product));
        }

        [Test]
        public async Task SelectProduct_ValidProduct_ExpectSelected_ExpectedInProgress()
        {
            var product = new Product
            {
                Name = "Pineapple",
                Cost = 5.5
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(PopulateProducts(product));

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(-5.5);

            var result = await _sut.SelectProductAsync(product);

            Assert.AreEqual(false, result.Accepted);
            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);
        }

        [Test]
        public async Task SelectProduct_ValidProduct_OnlyNameField_ExpectSelected()
        {
            var product = new Product
            {
                Name = "Gold bar"
            };

            _mockVendingMachineRepository.Stub(v => v.GetProducts())
                .ReturnAsync(PopulateProducts(product));

            _mockChangeCalculator.Stub(c => c.CalculateChange(Arg<Transaction>.Matches(t => t.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))))
                .Return(-5.5);

            var result = await _sut.SelectProductAsync(product);

            Assert.AreEqual(false, result.Accepted);
        }

        [Test]
        public void CancelPurchase_ExpectChangeReturned()
        {
            _sut.InsertCoin(2);
            _sut.InsertCoin(0.5);

            var change = _sut.CancelPurchase();

            Assert.AreEqual(2.5, change);
            Assert.AreEqual(0, _sut.CoinsEntered.Count);
        }

        [Test]
        public void CancelPurchase_ExpectTransactionStandBy()
        {
            _sut.InsertCoin(2);
            _sut.InsertCoin(0.5);

            Assert.AreEqual(TransactionState.InProgress, _sut.TransactionState);

            _sut.CancelPurchase();

            Assert.AreEqual(TransactionState.StandBy, _sut.TransactionState);
        }

        private List<Product> PopulateProducts(Product product)
        {
            var productList = _fixture.Create<List<Product>>();
            productList.Add(product);
            return productList;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Models;

namespace VendingMachine.Tests.Infrastructure
{
    public class ChangeCalculatorTests
    {
        private Fixture _fixture;

        private ChangeCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _sut = new ChangeCalculator();
        }

        [Test]
        public void CalculateChange_ExpectChangeCalculated_ExpectCorrectChange()
        {
            int cost = 2;
            var transaction = new Transaction
            {
                Product = new Product
                {
                    Name = "Pickle",
                    Cost = cost
                },
                CoinsEntered = new double[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                }
            };

            var expectedChange = transaction.CoinsEntered.Sum() - cost;        //Preferred something human readable over autofixture

            var productList = _fixture.Create<List<Product>>();
            productList.Add(transaction.Product);   //Ensure that our product is in there

            var result = _sut.CalculateChange(transaction);

            Assert.AreEqual(expectedChange, result);
        }
    }
}
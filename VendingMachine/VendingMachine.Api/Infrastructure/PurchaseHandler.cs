using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Api.Models;
using VendingMachine.Api.Repositories;

namespace VendingMachine.Api.Infrastructure
{
    public class PurchaseHandler : IPurchaseHandler
    {
        public TransactionState TransactionState { get; private set; }

        public List<double> CoinsEntered { get; }
        public Product SelectedProduct { get; private set; }

        private readonly IChangeCalculator _changeCalculator;
        private readonly IVendingMachineRepository _vendingMachineRepository;

        public PurchaseHandler(IChangeCalculator changeCalculator, IVendingMachineRepository vendingMachineRepository)
        {
            CoinsEntered = new List<double>();
            TransactionState = TransactionState.StandBy;

            _changeCalculator = changeCalculator;
            _vendingMachineRepository = vendingMachineRepository;
        }

        public PurchaseResult InsertCoin(double coin)
        {
            CoinsEntered.Add(coin);

            return HandlePurchase();
        }

        public async Task<PurchaseResult> SelectProductAsync(Product searchProduct)
        {
            var products = await _vendingMachineRepository.GetProducts();

            var resultProduct = products.Find(p => p.Name.Equals(searchProduct.Name, StringComparison.OrdinalIgnoreCase));

            if (resultProduct != null)
            {
                SelectedProduct = resultProduct;

                return HandlePurchase();
            }

            throw new KeyNotFoundException();
        }

        public double CancelPurchase()
        {
            SelectedProduct = null;

            var change = CoinsEntered.Sum();
            CoinsEntered.Clear();

            TransactionState = TransactionState.StandBy;

            return change;
        }

        private PurchaseResult HandlePurchase()
        {
            TransactionState = TransactionState.InProgress;

            var result = ProcessPurchase(CoinsEntered.ToArray());

            if (result != null && result.AmountDue >= 0)
            {
                TransactionState = TransactionState.StandBy;
            }

            return result;
        }

        private PurchaseResult ProcessPurchase(double[] coinsEntered)
        {
            if (SelectedProduct != null)
            {
                var change = _changeCalculator.CalculateChange(
                    new Transaction
                    {
                        Product = SelectedProduct,
                        CoinsEntered = coinsEntered
                    });

                var result = new PurchaseResult
                {
                    Accepted = change >= 0,
                    AmountDue = change
                };

                return result;
            }

            return null;
        }
    }
}
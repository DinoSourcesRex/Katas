using System.Linq;
using VendingMachine.Api.Models;

namespace VendingMachine.Api.Infrastructure
{
    public class ChangeCalculator : IChangeCalculator
    {
        public double CalculateChange(Transaction transaction)
        {
            return transaction.CoinsEntered.Sum() - transaction.Product.Cost;
        }
    }
}
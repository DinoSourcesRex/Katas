using System.Diagnostics;
using System.Linq;

namespace VendingMachine.Api.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Transaction
    {
        public Product Product { get; set; }
        public double[] CoinsEntered { get; set; }

        private string DebuggerDisplay => $"Product: {Product.Name} Cost: {Product.Cost:0.00} Paid: {CoinsEntered.Sum():0.00}";
    }
}
using System.Diagnostics;

namespace VendingMachine.Api.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Product
    {
        public string Name { get; set; }
        public double Cost { get; set; }

        private string DebuggerDisplay => $"Product: {Name} Cost: {Cost:0.00}";
    }
}
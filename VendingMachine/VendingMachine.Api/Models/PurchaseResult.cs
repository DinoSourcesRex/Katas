using System.Diagnostics;

namespace VendingMachine.Api.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PurchaseResult
    {
        public bool Accepted { get; set; }
        public double AmountDue { get; set; }

        private string DebuggerDisplay => $"Accepted: {Accepted} Cost: {AmountDue:0.00}";
    }
}
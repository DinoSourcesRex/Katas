namespace VendingMachine.Api.Infrastructure
{
    public class VendingMachineDisplay : IVendingMachineDisplay
    {
        public string Message { get; private set; }

        public void PrintPaymentDue(double paymentDue)
        {
            Message = $"INSERT £{paymentDue:+0.00;0.00}";
        }

        public void PrintInsertCoin()
        {
            Message = "INSERT COIN";
        }

        public void PrintGratitude()
        {
            Message = "THANK YOU";
        }
    }
}
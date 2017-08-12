namespace VendingMachine.Api.Infrastructure
{
    public interface IVendingMachineDisplay
    {
        string Message { get; }
        void PrintPaymentDue(double paymentDue);
        void PrintInsertCoin();
        void PrintGratitude();
    }
}
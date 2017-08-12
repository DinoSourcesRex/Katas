using VendingMachine.Api.Models;

namespace VendingMachine.Api.Infrastructure
{
    public interface IChangeCalculator
    {
        double CalculateChange(Transaction transaction);
    }
}
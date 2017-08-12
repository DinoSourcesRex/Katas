using VendingMachine.Api.Models;

namespace VendingMachine.Api.Infrastructure
{
    public interface IVendingMachineHardware
    {
        void EjectChange(double change);
        void EjectItem(Product product);
    }
}
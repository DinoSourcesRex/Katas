using VendingMachine.Api.Controllers;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Repositories;

namespace VendingMachine.Tests.Acceptance.StepData
{
    public class VendingMachineData
    {
        public IChangeCalculator ChangeCalculator;
        public IVendingMachineRepository VendingMachineRepository;
        public IPurchaseHandler PurchaseHandler;

        public IVendingMachineDisplay VendingMachineDisplay;
        public IVendingMachineHardware VendingMachineHardware;
        public VendingMachineController VendingMachine;
    }
}
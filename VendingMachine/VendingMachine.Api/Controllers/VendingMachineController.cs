using System.Threading.Tasks;
using VendingMachine.Api.Infrastructure;
using VendingMachine.Api.Models;

namespace VendingMachine.Api.Controllers
{
    public class VendingMachineController
    {
        private readonly IPurchaseHandler _purchaseHandler;
        private readonly IVendingMachineDisplay _vendingMachineDisplay;
        private readonly IVendingMachineHardware _vendingMachineHardware;

        public VendingMachineController(IPurchaseHandler purchaseHandler, IVendingMachineDisplay vendingMachineDisplay, IVendingMachineHardware vendingMachineHardware)
        {
            _purchaseHandler = purchaseHandler;
            _vendingMachineDisplay = vendingMachineDisplay;
            _vendingMachineHardware = vendingMachineHardware;
        }

        public async Task SelectProductAsync(Product product)
        {
            var result = await _purchaseHandler.SelectProductAsync(product);

            if (result.Accepted)
            {
                //This is purposely not awaited as there is a delay to simulate the machine resetting further now.
                CompletePurchaseAsync(result.AmountDue, product);
            }
            else
            {
                PrintPaymentDue(result.AmountDue);
            }
        }

        public async Task InsertCoinAsync(double coin)
        {
            var result = _purchaseHandler.InsertCoin(coin);

            if (result != null && result.Accepted)
            {
                await CompletePurchaseAsync(result.AmountDue, _purchaseHandler.SelectedProduct);
            }
        }

        public void CancelTransaction()
        {
            ReturnChange(_purchaseHandler.CancelPurchase());
            _vendingMachineDisplay.PrintInsertCoin();
        }

        private async Task CompletePurchaseAsync(double changeDue, Product product)
        {
            _vendingMachineDisplay.PrintGratitude();
            ReturnChange(changeDue);
            _vendingMachineHardware.EjectItem(product);

            await Task.Delay(800);  //Delay to simulate the machine resetting. Not really idea but there you have it.
            _vendingMachineDisplay.PrintInsertCoin();
        }

        private void PrintPaymentDue(double paymentDue)
        {
            _vendingMachineDisplay.PrintPaymentDue(paymentDue);
        }

        private void ReturnChange(double change)
        {
            _vendingMachineHardware.EjectChange(change);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Api.Models;

namespace VendingMachine.Api.Infrastructure
{
    public interface IPurchaseHandler
    {
        List<double> CoinsEntered { get; }
        Product SelectedProduct { get; }
        TransactionState TransactionState { get; }
        double CancelPurchase();
        PurchaseResult InsertCoin(double coin);
        Task<PurchaseResult> SelectProductAsync(Product searchProduct);
    }
}
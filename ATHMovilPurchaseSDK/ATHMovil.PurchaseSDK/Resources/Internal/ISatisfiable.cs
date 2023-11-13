using ATHMovil.Purchase.Model;
namespace ATHMovil.Purchase
{
    internal interface ISatisfiable
    {
        PurchaseException? IsSatisfy { get; }
    }
}

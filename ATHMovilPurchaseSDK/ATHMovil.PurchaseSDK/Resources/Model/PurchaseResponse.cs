namespace ATHMovil.Purchase.Model
{
    public sealed class PurchaseResponse
    {

        public Purchase Purchase { get; private set; }
        public PurchaseUser User { get; private set; }
        public PurchaseInfo Info { get; private set; }

        internal PurchaseResponse()
        {
        }

        internal PurchaseResponse(
            Purchase purchase,
            PurchaseUser user,
            PurchaseInfo info)
        {
            Purchase = purchase;
            User = user;
            Info = info;
        }
    }
}

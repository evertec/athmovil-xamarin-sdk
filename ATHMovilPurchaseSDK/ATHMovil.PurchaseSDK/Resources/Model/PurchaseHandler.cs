using System;
namespace ATHMovil.Purchase.Model
{
    public sealed class PurchaseHandler : ISatisfiable
    {
        public Action<PurchaseResponse> OnCompleted { get; private set; }
        public Action<PurchaseResponse> OnCancelled { get; private set; }
        public Action<PurchaseResponse> OnExpired { get; private set; }
        public Action<PurchaseResponse> OnFailed { get; private set; }
        public Action<PurchaseException> OnException { get; private set; }

        public PurchaseException? IsSatisfy
        {
            get
            {
                if (OnCompleted != null && OnCancelled != null && OnException != null && OnExpired != null)
                {
                    return null;
                }

                return PurchaseException.Request("The PurchaseHandler does not allow null");
            }
        }

        public PurchaseHandler(
            Action<PurchaseResponse> onCompleted,
            Action<PurchaseResponse> onCancelled,
            Action<PurchaseResponse> onExpired,
            Action<PurchaseException> onException)
        {
            OnCompleted = onCompleted;
            OnCancelled = onCancelled;
            OnExpired = onExpired;
            OnException = onException;
        }

        private PurchaseHandler()
        {
        }
    }
}

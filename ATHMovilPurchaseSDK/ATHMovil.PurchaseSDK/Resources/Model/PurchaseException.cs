using ATHMovil.Purchase.Storage;
using ATHMovil.Purchase.Utils;
using ATHMovil.PurchaseSDK.String;

namespace ATHMovil.Purchase.Model
{
    public struct PurchaseException
    {
        public string FailureReason { get; set; }
        public string Description { get; set; }
        public bool IsRequestError { get; set; }

        internal static PurchaseException Request(string description)
        {
            string title = SDKGlobal.Instance().Flow.Equals("Yes") ? StringMensaje.GetGenericErrorTitle() : "ATH Móvil";
            return new PurchaseException()
            {
                FailureReason = title,
                Description = description,
                IsRequestError = true
            };
        }

        internal static PurchaseException Response(string description)
        {
            string title = SDKGlobal.Instance().Flow.Equals("Yes") ? StringMensaje.GetGenericErrorTitle() : "ATH Móvil";
            return new PurchaseException()
            {
                FailureReason = title,
                Description = description,
                IsRequestError = false
            };
        }
    }
}

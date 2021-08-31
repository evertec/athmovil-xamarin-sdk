using ATHMovil.Purchase.Extension;
using ATHMovil.Purchase.Constants;
using ATHMovil.Purchase.Openers;
using System;
using ATHMovil.Purchase.Decoders;
using Xamarin.Essentials;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Model.Manager;


namespace ATHMovil.Purchase.Model
{
    public struct PurchaseRequest : ISatisfiable
    {
        public Purchase Purchase { get; private set; }
        public Business Business { get; private set; }
        public UriCallBack URLScheme { get; private set; }
        internal PurchaseHandler Handler { get; private set; }
        public double TimeOut { get; private set; }
        internal Guid PaymentId { get; private set; }

        public PurchaseException? IsSatisfy
        {
            get
            {
                if (!TimeOut.IsBetween(DefaultValues.MinTimeOut, DefaultValues.MaxTimeOut))
                {
                    return PurchaseException.Request("Timeout data type value is invalid");
                }

                if (Purchase == null)
                {
                    return PurchaseException.Request("You should provide a Purchase object");
                }

                PurchaseException? purchaseError = Purchase.IsSatisfy;
                PurchaseException? businessError = Business.IsSatisfy;
                PurchaseException? urlSchemeError = URLScheme.IsSatisfy;

                if (purchaseError != null)
                {
                    return purchaseError;
                }

                if (businessError != null)
                {
                    return businessError;
                }

                if (urlSchemeError != null)
                {
                    return urlSchemeError;
                }

                return null;
            }
        }

        public PurchaseRequest(
            Purchase purchase,
            Business token,
            UriCallBack scheme)
        {
            Purchase = purchase;
            Business = token;
            URLScheme = scheme;
            TimeOut = DefaultValues.MaxTimeOut;
            Handler = null;
            PaymentId = Guid.NewGuid();
        }

        public void Pay(PurchaseHandler handler) 
        {
            Pay(handler, TimeOut);
        }

        public void Pay(PurchaseHandler handler, double timeout)
        {
            TimeOut = timeout;

            if (handler == null || handler.IsSatisfy != null)
            {
                return;
            }

            PurchaseException? purchaseError = IsSatisfy;
            if (purchaseError != null)
            {
                handler.OnException(purchaseError.Value);
                return;
            }

            Handler = handler;

            OpenerResult result = CrossOpener.Current.OpenATHMovil(this, PurchaseManager.SharedInstance.TargetEnviroment);

            switch (result)
            {
                case OpenerResult.ATHMovilPersonal:
                    PurchaseManager.SharedInstance.CurrentRequest = this;
                    break;
                case OpenerResult.NotSupportedPlatform:
                    handler.OnException(PurchaseException.Request("The current device is not supported"));
                    break;
                case OpenerResult.Error:
                    handler.OnException(PurchaseException.Request("There is an error creating the request for ATH Móvil"));
                    break;
            }
        }

        /// <summary>
        /// Proccess the responseBody that contiants the response JSON
        /// </summary>
        /// <param name="responseBody">Current JSON Body</param>
        internal void Paid(string responseBody)
        {
            
            PurchaseResponse response = CrossDecoder.Current.Decode(responseBody);

            if (response == null)
            {
                Handler.OnException(PurchaseException.Response("Error converting the response from ATH Movil"));
                return;
            }

            switch (response.Info.Status)
            {
                case PurchaseState.cancelled:
                    Handler.OnCancelled(response);
                    break;
                case PurchaseState.completed:
                    Handler.OnCompleted(response);
                    break;
                case PurchaseState.expired:
                    Handler.OnExpired(response);
                    break;
            }
        }
    }
}


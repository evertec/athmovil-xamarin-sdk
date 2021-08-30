using Xamarin.Essentials;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Encoders;
using UIKit;
using Foundation;

namespace ATHMovil.Purchase.Openers
{
    internal class Opener : IOpener
    {
        private const string AppleStoreUri = "itms://itunes.apple.com/sg/app/ath-movil/id658539297?l=zh&mt=8";
        private const string ParameterJSONName = "transaction_data";
        private const string PathRealToken = "mobile";
        private const string PathDummyToken = "mobileDummy";

        internal Opener()
        {
        }

        /// <summary>
        /// Opens ATH Movil with universal links if it can not open ATH Movil it will open Apple Store
        /// This method it is a mix between Swift and Essentials.
        /// </summary>
        /// <param name="request">Current request that containts the payment information</param>
        /// <param name="target">Current target for payment</param>
        /// <returns>Always returns a ATHMovilPersonal as status because it is not possible to determine when the SDK
        /// could not open ATH Movil Personal, the method UIApplication.SharedApplication.OpenURLAsync never works
        /// </returns>
        OpenerResult IOpener.OpenATHMovil(PurchaseRequest request, ATHMovilTarget target)
        {
            string bodyJSON = Encoder.Encode(request);

            string fullPath = target.GetTargetUniversalLink() + GetPathPaymentType(request.Business);
            NSUrlComponents components = new NSUrlComponents(fullPath);
            components.QueryItems = new NSUrlQueryItem[1] { new NSUrlQueryItem(Opener.ParameterJSONName, bodyJSON) };

            OpenATHMovil(components.Url);

            return OpenerResult.ATHMovilPersonal;
        }

        /// <summary>
        /// It opens ATH Movil Personal sending the payment information, if it could not open ATH Movil will
        /// open Apple store 
        /// </summary>
        /// <param name="url">URL that containts the payment information </param>
        private void OpenATHMovil(NSUrl url)
        {
            UIApplicationOpenUrlOptions options = new UIApplicationOpenUrlOptions();
            options.UniversalLinksOnly = true;

            UIApplication.SharedApplication.OpenUrl(url, options, OpenAppStore);
        }

        /// <summary>
        /// if canOpen is false it will open Apple store with the URL of ATH Movil Application 
        /// </summary>
        /// <param name="canOpen">True if the SDK could open ATH Movil</param>
        private void OpenAppStore(bool canOpen)
        {
            if (canOpen)
            {
                return;
            }
            UIApplication.SharedApplication.OpenUrl(new NSUrl(AppleStoreUri));
        }

        /// <summary>
        /// Returns the path of the current payment base on the business token if it is dummy token
        /// will return UriSchemeDummy otherwise UriScheme
        /// </summary>
        /// <param name="business">Current business token selected by the customer</param>
        /// <returns></returns>
        private string GetPathPaymentType(Business business)
        {
            if (business.IsDummy)
            {
                return PathDummyToken;
            }
            return PathRealToken;
        }
    }

    internal static class ATHMovilTargetExtension
    {
        /// <summary>
        /// Returns a string with the universal link of ATH Móvil Personal base on target enum
        /// </summary>
        /// <param name="target">Target for QA, PILOT and Production</param>
        /// <returns>Depends on the target will return an URL that containts the version of ATH Movil</returns>
        internal static string GetTargetUniversalLink(this ATHMovilTarget target)
        {
            return "https://athm-ulink-prod-static-website.s3.amazonaws.com/e-commerce/";
        }
    }
}

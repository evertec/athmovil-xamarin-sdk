using System;
using Android.Content.PM;
using Android.Content;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Encoders;

namespace ATHMovil.Purchase.Openers
{
    internal class Opener : IOpener
    {
        private const string PlayStoreUri = "market://details?id=com.evertec.athmovil.android";
        private const string ParameterJSONName = "jsonData";
        private const string ParameterBundleName = "bundleID";
        private const string ParameterTimeoutName = "purchaseTimeOut";

        internal Opener()
        {
        }

        /// <summary>
        /// This method is a mix between Java and Essentials, this method convert
        /// the purchase in query string and send the payment to ATH Movil Personal
        /// </summary>
        /// <param name="request">Current purchase request</param>
        /// <param name="target">It could be QA, Pilot o Production</param>
        /// <returns>Returns ATHMovilPersonal if it can open ATH Movil Application</returns>
        OpenerResult IOpener.OpenATHMovil(PurchaseRequest request, ATHMovilTarget target)
        {
            string packageName = target.GetTargetBundle();

            
            if (!IsAppInstalled(packageName))
            {
                Intent playStoreIntent = new Intent(Intent.ActionView, global::Android.Net.Uri.Parse(PlayStoreUri));
                playStoreIntent.SetFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(playStoreIntent);
                return OpenerResult.ApplicationsStore;
            }

            try
            {
                PackageManager pm = Android.App.Application.Context.PackageManager;
                Intent intent = pm.GetLaunchIntentForPackage(packageName);
                if (intent != null)
                {
                    /// Timeout for Android must be a long because Intent keeps the parameter type
                    long timeout = Convert.ToInt64(request.TimeOut * 1000.0);
                    string requestBody = Encoder.Encode(request);
                    intent.PutExtra(Opener.ParameterBundleName, Android.App.Application.Context.PackageName);
                    intent.PutExtra(Opener.ParameterJSONName, requestBody);
                    intent.PutExtra(Opener.ParameterTimeoutName, timeout);
                    intent.SetFlags(ActivityFlags.ClearTop);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    Android.App.Application.Context.StartActivity(intent);
                    return OpenerResult.ATHMovilPersonal;
                }

                return OpenerResult.Error;
            }
            catch (ActivityNotFoundException)
            {
                return OpenerResult.Error;
            }
        }
        /// <summary>
        /// Verify if ATH Movil Applicaiton is open in this case returns true otherwise false
        /// </summary>
        /// <param name="packageName">Package name that will check if one activity response to this packageName</param>
        /// <returns>Returns true if there is ona application that response to this packageName</returns>
        
        [Obsolete]
        private static bool IsAppInstalled(string packageName)
        {
            PackageManager pm = Android.App.Application.Context.PackageManager;

            try
            {
                pm.GetPackageInfo(packageName, PackageInfoFlags.Activities);
                return true;
            }
            catch (PackageManager.NameNotFoundException)
            {
                return false;
            }
        }
    }

    internal static class ATHMovilTargetExtension
    {
        /// <summary>
        /// Returns the bundle target depends on the target parameter
        /// </summary>
        /// <param name="target">current target that the sdk will open</param>
        /// <returns>Returns the bundle of Android</returns>
        internal static string GetTargetBundle(this ATHMovilTarget target)
        {
            switch (target)
            {
                case ATHMovilTarget.DEV:
                    return "com.evertec.athmovil.android.qa";
                case ATHMovilTarget.QA:
                    return "com.evertec.athmovil.android.qa";
                case ATHMovilTarget.Pilot:
                    return "com.evertec.athmovil.android.piloto";
                default:
                    return "com.evertec.athmovil.android";
            }
        }
    }
}

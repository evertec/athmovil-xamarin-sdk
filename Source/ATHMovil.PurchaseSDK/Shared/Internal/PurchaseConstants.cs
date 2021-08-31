
namespace ATHMovil.Purchase.Constants
{
    internal static class DefaultValues
    {
        internal const double MaxTimeOut = 600, MinTimeOut = 60;
    }

    /// <summary>
    /// Constants for ATH Movil Application
    /// </summary>
    internal static class UriTargetsScheme
    {
        
        public const string GitHubReposEndpoint = "https://api.github.com/orgs/dotnet/repos";
        internal const string WebServicePath = "webservice.htm";
    }

    internal static class StringsConstants
    {
        internal const string PosfixEs = "Es.png";
        internal const string PosfixEn = "En.png";
        internal const string Version = "Version";
    }

    internal static class ResourcesUI
    {
        /// <summary>
        /// Full path of images, it must be the workspace and the folder, it is the full
        /// name of each image. In the code append the StringsConstants.PosfixEng base on
        /// the configuration for example ATHMovil.PurchaseSDK.Images.PrimarySpa.png
        /// </summary>
        internal const string PrimaryImage = "ATHMovil.PurchaseSDK.Shared.Images.Primary";
        internal const string SecondaryImage = "ATHMovil.PurchaseSDK.Shared.Images.Secondary";
    }
}

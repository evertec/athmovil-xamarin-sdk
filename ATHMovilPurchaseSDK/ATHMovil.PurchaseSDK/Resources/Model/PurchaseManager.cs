using System.Collections.Specialized;
using ATHMovil.Purchase.Openers;
using ATHMovil.Purchase.Extension;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Utils;
using ATHMovil.Purchase.Storage;

namespace ATHMovil.Purchase.Model.Manager
{
    public sealed class PurchaseManager
    {
        private static readonly Lazy<PurchaseManager> lazy = new Lazy<PurchaseManager>(() => new PurchaseManager(), LazyThreadSafetyMode.PublicationOnly);
        public static PurchaseManager SharedInstance => lazy.Value;

        private static string TopicOnResume = "ResumeEvent";
        public static bool isRequestWithData = true;

        internal Uri CurrentTarget;
        internal string CurrentAWSTarget;
        internal ATHMovilTarget targetEnviroment;
        /// <summary>
        /// ATH Movil enviroment it could be QA, Pilot or Production. For client implementation should be Production
        /// avoid to use QA or Pilot, 
        /// </summary>
        public ATHMovilTarget TargetEnviroment {
            get => targetEnviroment;
            set
            {
                targetEnviroment = value;
                CurrentTarget = value.GetTargetDomain();
                CurrentAWSTarget = value.GetAWSDomain();
            }
        }

        private IOpener _opener { get; set; }
        internal IOpener Opener
        {
            get {
                return _opener;
            }

            set {
                _opener = value;
            }
        }

        internal PurchaseRequest? _currentRequest { get; set; }


        internal PurchaseRequest? CurrentRequest
        {
            get => _currentRequest;
            set
            {
                _currentRequest = value;

                if (value != null)
                {
                    //Task.Delay(100);
                    if (isRequestWithData == false) {
                        ObserveOnResume();
                    }
                }
            }
        }

        private const string iOSResponseParameter = "athm_payment_data";

        /// <summary>
        /// Convert the reponseUri to a PurchaseResponse if the responseUri is not a
        /// response form ATH Movil will response false otherwise true
        /// </summary>
        /// <param name="responseUri">Current URL that comes from ATH Movil Application</param>
        /// <returns>Returns false if the URL is not from ATH Movil Application</returns>

        public static bool tryComplete(Uri responseUri) 
        {
            StringDictionary parameters = responseUri.GetQueryParams();
            string responseBody = parameters[iOSResponseParameter];
            
            if (string.IsNullOrEmpty(responseBody) || SharedInstance.CurrentRequest == null)
            {
                isRequestWithData = false;
                return false;
            }

            PurchaseRequest currentRequest = (PurchaseRequest)SharedInstance.CurrentRequest;
            SharedInstance.CurrentRequest = null;

            currentRequest.Paid(responseBody);

            return true;
        }

        /// <summary>
        /// Convert the response to a PurchaseResponse if the response is not a
        /// response form ATH Movil will response false otherwise true 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        ///
        public static bool tryComplete(string response)
        {
            if (string.IsNullOrEmpty(response) || SharedInstance.CurrentRequest == null)
            {
                isRequestWithData = false;
                return false;
            }

            PurchaseRequest currentRequest = (PurchaseRequest)SharedInstance.CurrentRequest;
            SharedInstance.CurrentRequest = null;

            currentRequest.Paid(response);

            return true;
        }

        /// <summary>
        /// This method is calling everytime that the client make a payment or
        /// everytime the SDK has a pending payment
        /// </summary>
        private void ObserveOnResume()
        {
            MessagingCenter.Subscribe<PurchaseManager>(this, PurchaseManager.TopicOnResume, async manager =>
            {
               
                MessagingCenter.Unsubscribe<PurchaseManager>(this, PurchaseManager.TopicOnResume);
                PurchaseRequest? currentRequest = manager.CurrentRequest;
                manager.CurrentRequest = null;

                RestService restService = new RestService();
                PurchaseResponse response = await restService.CallVerificationServiceAsync(currentRequest);
                
            });
        }

        /// <summary>
        /// Method to call the web service to get the current status of the transaction
        /// </summary>
        public static void OnResume()
        {
            MessagingCenter.Send(PurchaseManager.SharedInstance, PurchaseManager.TopicOnResume);
        }

        private PurchaseManager()
        {
            TargetEnviroment = ATHMovilTarget.Production;
        }
    }
}

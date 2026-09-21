using Newtonsoft.Json;
using System.Text;
using ATHMovil.Purchase.Storage;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Manager;
using System.Diagnostics;

namespace ATHMovil.Purchase.Utils
{
    public class FindPaymentServices : BindableObject
    {

        public Command CallApiCommand { get; set; }
        internal HttpClient _client;
        private string Host
        {
            get
            {
                return PurchaseManager.SharedInstance.CurrentAWSTarget;
            }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                OnIsBusyChanged();
            }
        }

        public FindPaymentServices()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            _client = new HttpClient(httpClientHandler);
            _client.Timeout = TimeSpan.FromSeconds(30);

            _isBusy = true;
        }
        
        public async Task<PurchaseResponse> FindPaymentServicesCall(PurchaseResponse responsePurchase)
        {
            return await CallApi(responsePurchase);
        }

        private void OnIsBusyChanged()
        {
            if (IsBusy)
            {
                LoadingView.ShowLoadingOverlay();
            }
            else
            {
                LoadingView.HideLoadingOverlay();
            }
        }

        internal async Task<PurchaseResponse> CallApi(PurchaseResponse responsePurchase)
        {
            if (SDKGlobal.Instance().Token == null) {
                IsBusy = false;
                return null;
            }

            IsBusy = true;

            try
            {
                var request = new FindPaymentRequest{
                    ecommerceId = SDKGlobal.Instance().EcommerceID,
                    publicToken = SDKGlobal.Instance().PublicToken
                };
                string json = JsonConvert.SerializeObject(request);
                Console.WriteLine($"Datos de find payment: {json}");
                HttpContent callContent = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    // Agregar la cabecera "Host"
                    client.DefaultRequestHeaders.Add("Host", Host);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SDKGlobal.Instance().Token);
                    String url = "https://" + Host + "/api/business-transaction/ecommerce/business/findPayment";
                    HttpResponseMessage response = await client.PostAsync(url, callContent);

                    FindPaymentServices.PrintDebug(url, response, await response.Content.ReadAsStringAsync());

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AuthorizationResponse>(content);
                        return ProcessAuthorizationResult(result, responsePurchase);
                    }

                    responsePurchase.Info.Status = PurchaseState.failed;
                    return responsePurchase;
                }
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                return null;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private PurchaseResponse ProcessAuthorizationResult(AuthorizationResponse result, PurchaseResponse responsePurchase)
        {
            var rawStatus = result?.Data?.EcommerceStatus;
            var status = PurchaseInfo.ParseStatus(rawStatus);

            responsePurchase.Info.Status = status;

            if (status == PurchaseState.success || status == PurchaseState.completed)
            {
                // Mapeo seguro de valores exitosos
                _ = int.TryParse(result.Data.DailyTransactionId, out int dailyId);
                
                responsePurchase.Info.DailyTransactionID = dailyId;
                responsePurchase.Info.ReferenceNumber = result.Data.ReferenceNumber ?? string.Empty;
                responsePurchase.Purchase.NetAmount = result.Data.NetAmount;
                responsePurchase.Purchase.Fee = result.Data.Fee;
            }

            return responsePurchase;
        }

        public static void PrintDebug(string url, HttpResponseMessage response, string responseBody)
        {
            #if DEBUG
                Debug.WriteLine("========== HTTP DEBUG ==========");
                Debug.WriteLine($"URL: {url}");
                Debug.WriteLine($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
                Debug.WriteLine($"Response: {responseBody}");
                Debug.WriteLine($"Token: {SDKGlobal.Instance().Token}");
                Debug.WriteLine("================================");
            #endif
        }
    }
}


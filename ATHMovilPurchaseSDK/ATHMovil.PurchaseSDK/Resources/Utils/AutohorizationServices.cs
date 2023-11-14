using Newtonsoft.Json;
using System.Text;
using ATHMovil.Purchase.Storage;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Manager;
using ATHMovil.PurchaseSDK.String;
using System;
using System.Threading.Tasks;

namespace ATHMovil.Purchase.Utils
{
    public class AuthorizationServices : BindableObject
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

        public AuthorizationServices()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            _client = new HttpClient(httpClientHandler);
            _client.Timeout = TimeSpan.FromSeconds(30);

            _isBusy = true;
        }

        public async Task<AuthorizationResponse> AuthorizationServicesCall()
        {
            return await CallApi();
        }

        private void OnIsBusyChanged(){
            if (IsBusy){
                LoadingView.ShowLoadingOverlay();
            }else{
                LoadingView.HideLoadingOverlay();
            }
        }

        internal async Task<AuthorizationResponse> CallApi()
        {
            if (SDKGlobal.Instance().Token == null) {
                IsBusy = false;
                return null;
            }

            IsBusy = true;

            try
            {
                HttpContent callContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    // Agregar la cabecera "Host"
                    client.DefaultRequestHeaders.Add("Host", Host);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + SDKGlobal.Instance().Token);
                    HttpResponseMessage response = await client.PostAsync("https://" + Host + "/api/business-transaction/ecommerce/authorization", callContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AuthorizationResponse>(content);
                        return result;
                    }
                    else
                    {
                        return null;
                    }
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
    }
}


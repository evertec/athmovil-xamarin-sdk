using ATHMovil.Purchase.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATHMovil.Purchase.Model.Manager;
using System.Collections.Generic;

using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace ATHMovil.Purchase.Utils
{
    public class RestService
    {
        internal HttpClient _client;

        private const string ecommerceTransaction = "rs/eCommerceTransfer/consultTransactionStatus/";
        private Uri OnResumeEndpoint
        {
            get
            {
                string serverUri = PurchaseManager.SharedInstance.CurrentTarget.AbsoluteUri;
                string endPoint = $"{ serverUri + ecommerceTransaction }";
                return new Uri(endPoint);
            }
        }

        class JsonProperties{
            public String publicToken { get; set; }
            public String paymentID { get; set; }
        }

        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            _client = new HttpClient(httpClientHandler);
            _client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<PurchaseResponse> CallVerificationServiceAsync(PurchaseRequest? currentRequest)
        {
            
            if (currentRequest == null || !currentRequest.HasValue)
            {
                return null;
            }

            if (currentRequest.Value.Business.IsDummy)
            {
                return GetResponseDummy(currentRequest.Value);
            }

            return await GetPurchaseRespondeFromService(currentRequest.Value);
        }

        /// <summary>
        /// Returns the puchase with data dummy this is for the web service when the token is dummy
        /// </summary>
        /// <param name="currentRequest">Current request of the SDK see PurchaseManager.SharedInstance.CurrentRequest</param>
        /// <returns>Return an instance of PurchaseResponse with data Dummy and it always is completed</returns>
        internal PurchaseResponse GetResponseDummy(PurchaseRequest currentRequest)
        {
            double feePercentage = 0.0225;
            
            currentRequest.Purchase.Fee = Math.Round((currentRequest.Purchase.Total * feePercentage), 2);
            currentRequest.Purchase.NetAmount = currentRequest.Purchase.Total - currentRequest.Purchase.Fee;

            PurchaseInfo purchaseInfo = new PurchaseInfo(PurchaseState.completed, 1, DateTime.Now, "000000-00000000abcd");
            PurchaseUser userDummy = new PurchaseUser("Valeria Herrero", "(787) 123-4567", "email@example.com");
            PurchaseResponse dummyResponse = new PurchaseResponse(currentRequest.Purchase, userDummy, purchaseInfo);

            currentRequest.Handler.OnCompleted(dummyResponse);

            return dummyResponse;
        }

        /// <summary>
        /// Call the endpoint ecommerceTransaction getting the purchase response from the web service
        /// </summary>
        /// <param name="currentRequest">Current request of the SDK see PurchaseManager.SharedInstance.CurrentRequest</param>
        /// <returns>Returns an instance of PurchaseResponse, it could be completed, cancelled or report the transacion as error</returns>
        internal async Task<PurchaseResponse> GetPurchaseRespondeFromService(PurchaseRequest currentRequest)
        {
            try
            {
                JsonProperties properties = new JsonProperties();
                properties.publicToken = currentRequest.Business.PublicToken;
                properties.paymentID = currentRequest.PaymentId.ToString();
                string json = JsonConvert.SerializeObject(properties);
                HttpContent callContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(OnResumeEndpoint, callContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (!responseContent.Contains("error"))
                    {
                        JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                        {
                            Error = delegate (object sender, ErrorEventArgs args)
                            {
                                args.ErrorContext.Handled = true;
                            }
                        };

                        Response responseFull = JsonConvert.DeserializeObject<Response>(responseContent, jsonSettings);

                        if (responseFull == null)
                        {
                            return null;
                        }

                        PurchaseResponse purchaseResponse = responseFull.Map();
                        switch (purchaseResponse.Info.Status)
                        {
                            case PurchaseState.completed:
                                currentRequest.Handler.OnCompleted(purchaseResponse);
                                break;
                            case PurchaseState.cancelled:
                                currentRequest.Handler.OnCancelled(purchaseResponse);
                                break;
                            case PurchaseState.expired:
                                currentRequest.Handler.OnExpired(purchaseResponse);
                                break;
                            case PurchaseState.failed:
                                currentRequest.Handler.OnFailed(purchaseResponse);
                                break;
                            default:
                                currentRequest.Handler.OnCancelled(purchaseResponse);
                                break;
                        }
                        return purchaseResponse;
                    }
                    else
                    {
                        PurchaseInfo error = new PurchaseInfo(PurchaseState.cancelled, 0, DateTime.Now, string.Empty);
                        PurchaseResponse errorResponse = new PurchaseResponse(currentRequest.Purchase, new PurchaseUser(), error);
                        currentRequest.Handler.OnCancelled(errorResponse);
                        return errorResponse;
                    }
                } else
                {
                    PurchaseInfo error = new PurchaseInfo(PurchaseState.cancelled, 0, DateTime.Now, string.Empty);
                    PurchaseResponse errorResponse = new PurchaseResponse(currentRequest.Purchase, new PurchaseUser(), error);
                    currentRequest.Handler.OnCancelled(errorResponse);
                    return errorResponse;
                }
            }
            catch (Exception)
            {
                PurchaseException purchaseException = new PurchaseException()
                {
                    Description = "Error getting response from webservice",
                    FailureReason = "NETWORK ERROR",
                    IsRequestError = false
                };
                currentRequest.Handler.OnException(purchaseException);
            }

            return null;
        }

        private class Response
        {
            [JsonProperty("name")]
            public string Name { get; private set; }

            [JsonProperty("phoneNumber")]
            public string Telephone { get; private set; }

            [JsonProperty("email")]
            public string Email { get; private set; }

            [JsonProperty("total")]
            internal double Total { get; set; }

            [JsonProperty("subTotal")]
            internal double SubTotal { get; set; }

            [JsonProperty("tax")]
            internal double Tax { get; set; }

            [JsonProperty("fee")]
            internal double Fee { get; set; }

            [JsonProperty("netAmount")]
            internal double NetAmount { get; set; }

            [JsonProperty("items")]
            internal List<ResponseItem> Items { get; set; }

            [JsonProperty("metadata1")]
            internal string Metadata1 { get; set; }

            [JsonProperty("metadata2")]
            internal string Metadata2 { get; set; }

            [JsonProperty("dailyTransactionID")]
            public int DailyTransactionID { get; private set; }

            [JsonProperty("referenceNumber")]
            public string ReferenceNumber { get; private set; }

            [JsonProperty("date")]
            public DateTime TransactionDate { get; private set; }

            [JsonProperty("status")]
            public PurchaseState Status { get; private set; }

            internal PurchaseResponse Map() {

                List<PurchaseItem> listItems = new List<PurchaseItem>();
                foreach (var item in Items)
                {
                    PurchaseItem newItem = new PurchaseItem(item.Name, item.Quantity, item.Price)
                    {
                        Description = item.Description,
                        Metadata = item.Metadata
                    };
                    listItems.Add(newItem);
                }

                PurchaseUser user = new PurchaseUser(Name, Telephone, Email);
                PurchaseInfo info = new PurchaseInfo(Status, DailyTransactionID, TransactionDate, ReferenceNumber);
                Model.Purchase purchase = new Model.Purchase(Total)
                {
                    SubTotal = SubTotal,
                    Tax = Tax,
                    Fee = Fee,
                    NetAmount = NetAmount,
                    Metadata1 = Metadata1,
                    Metadata2 = Metadata2,
                    Items = listItems
                };

                return new PurchaseResponse(purchase, user, info);
            }
        }


        private struct ResponseItem
        {
            [JsonProperty("name")]
            internal string Name { get; set; }
            [JsonProperty("price")]
            internal double Price { get; set; }
            [JsonProperty("quantity")]
            internal int Quantity { get; set; }
            [JsonProperty("metadata")]
            internal string Metadata { get; set; }
            [JsonProperty("description")]
            internal string Description { get; set; }
        }

    }
}

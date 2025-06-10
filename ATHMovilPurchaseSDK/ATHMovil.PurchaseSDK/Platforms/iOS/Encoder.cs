using System.Collections.Generic;
using ATHMovil.Purchase.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ATHMovil.Purchase.Storage;

namespace ATHMovil.Purchase.Encoders
{
    public static class Encoder
    {

        internal static string Encode(PurchaseRequest request)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    PurchaseException purchaseError = PurchaseException.Request(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                    request.Handler.OnException(purchaseError);
                }
            };

            string strJson = "";

            if (!request.Business.PublicToken.ToLower().Equals("dummy")){
                PurchaseBodySecure purchaseCodable = new PurchaseBodySecure(request);
                strJson = JsonConvert.SerializeObject(purchaseCodable, jsonSettings);
            }else {
                PurchaseBody purchaseCodable = new PurchaseBody(request);
                strJson = JsonConvert.SerializeObject(purchaseCodable, jsonSettings);
            }
               
            return strJson;
        }

        private struct PurchaseBody
        {
            [JsonIgnore]
            internal PurchaseRequest Request { get; set; }

            [JsonProperty("expiresIn")]
            internal double Timeout => Request.TimeOut;

            [JsonProperty("timeout")]
            internal double TimeoutSecure => Request.TimeOut;

            [JsonProperty("publicToken")]
            internal string PublicToken => Request.Business.PublicToken;

            [JsonProperty("scheme")]
            internal string Scheme => Request.URLScheme.Scheme;

            [JsonProperty("version")]
            internal string Version => "3.0";

            [JsonProperty("ecommerceId")]
            internal string EcommerceId => Request.Purchase.EcommerceId;

            [JsonProperty("phoneNumber")]
            internal string PhoneLine => Request.Purchase.PhoneNumber;

            [JsonProperty("items")]
            internal List<ItemBody> Items
            {
                get
                {
                    List<ItemBody> itemsReturn = new List<ItemBody>();

                    if (Request.Purchase.Items != null && Request.Purchase.Items.Count() > 0) {
                        foreach (var item in Request.Purchase.Items)
                        {
                            ItemBody newItem = new ItemBody()
                            {
                                Name = item.Name,
                                Description = item.Description,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Metadata = item.Metadata
                            };
                            itemsReturn.Add(newItem);
                        }
                    }

                    return itemsReturn;
                }
            }

            [JsonProperty("tax")]
            internal double Tax => Request.Purchase.Tax;

            [JsonProperty("subtotal")]
            internal double SubTotal => Request.Purchase.SubTotal;

            [JsonProperty("total")]
            internal double Total => Request.Purchase.Total;

            [JsonProperty("metadata1")]
            internal string Metadata1 => Request.Purchase.Metadata1;

            [JsonProperty("metadata2")]
            internal string Metadata2 => Request.Purchase.Metadata2;

            [JsonProperty("traceId")]
            internal string PaymentId => Request.PaymentId.ToString();

            internal PurchaseBody(PurchaseRequest request) => Request = request;
        }

        private struct ItemBody
        {
            [JsonProperty("name")]
            internal string Name { get; set; }
            [JsonProperty("price")]
            internal double Price { get; set; }
            [JsonProperty("quantity")]
            internal int Quantity { get; set; }
            [JsonProperty("metadata")]
            internal string Metadata { get; set; }
            [JsonProperty("desc")]
            internal string Description { get; set; }
        }

        private struct PurchaseBodySecure
        {
            [JsonIgnore]
            internal PurchaseRequest Request { get; set; }

            [JsonProperty("expiresIn")]
            internal double Timeout => Request.TimeOut;

            [JsonProperty("timeout")]
            internal double TimeoutSecure => Request.TimeOut;

            [JsonProperty("publicToken")]
            internal string PublicToken => Request.Business.PublicToken;

            [JsonProperty("scheme")]
            internal string Scheme => Request.URLScheme.Scheme;

            [JsonProperty("version")]
            internal string Version => "3.0";

            [JsonProperty("ecommerceId")]
            internal string EcommerceId => Request.Purchase.EcommerceId;

            [JsonProperty("phoneNumber")]
            internal string PhoneLine => Request.Purchase.PhoneNumber;

            internal PurchaseBodySecure(PurchaseRequest request) => Request = request;
        }
    }
}




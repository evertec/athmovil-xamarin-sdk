using System.Collections.Generic;
using ATHMovil.Purchase.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ATHMovil.Purchase.Encoders
{
    public static class Encoder
    {

        internal static string Encode(PurchaseRequest request)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    PurchaseException purchaseError = PurchaseException.Request(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                    request.Handler.OnException(purchaseError);
                }
            };

            PurchaseBody purchaseCodable = new PurchaseBody(request);
            return JsonConvert.SerializeObject(purchaseCodable, jsonSettings);
        }

        private struct PurchaseBody
        {
            [JsonIgnore]
            internal PurchaseRequest Request { get; set; }

            [JsonProperty("expiresIn")]
            internal double Timeout => Request.TimeOut;

            [JsonProperty("publicToken")]
            internal string PublicToken => Request.Business.PublicToken;

            [JsonProperty("scheme")]
            internal string Scheme => Request.URLScheme.Scheme;

            [JsonProperty("version")]
            internal string Version => "3.0";

            [JsonProperty("items")]
            internal List<ItemBody> Items
            {
                get
                {
                    List<ItemBody> itemsReturn = new List<ItemBody>();

                    foreach (var item in Request.Purchase.Items)
                    {
                        ItemBody newItem = new ItemBody() {
                            Name = item.Name,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Metadata = item.Metadata
                        };
                        itemsReturn.Add(newItem);
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
    }
}




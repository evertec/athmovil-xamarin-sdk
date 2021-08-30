using System.Collections.Generic;
using ATHMovil.Purchase.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ATHMovil.Purchase.Encoders
{
    internal static class Encoder
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

            [JsonProperty("businessToken")]
            internal string PublicToken => Request.Business.PublicToken;

            [JsonProperty("callbackSchema")]
            internal string Scheme => Request.URLScheme.Scheme;

            [JsonProperty("itemsSelectedList")]
            internal List<ItemBody> Items
            {
                get
                {
                    List<ItemBody> itemsReturn = new List<ItemBody>();

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

                    return itemsReturn;
                }
            }

            [JsonProperty("paymentId")]
            internal string PaymentId => Request.PaymentId.ToString();

            [JsonProperty("timeout")]
            internal double Timeout => Request.TimeOut;

            [JsonProperty("tax")]
            internal string Tax => $"{ Request.Purchase.Tax }";

            [JsonProperty("subtotal")]
            internal string SubTotalJSON => $"{ Request.Purchase.SubTotal }";

            [JsonProperty("total")]
            internal double Total => Request.Purchase.Total;

            [JsonProperty("metadata1")]
            internal string Metadata1 => Request.Purchase.Metadata1;

            [JsonProperty("metadata2")]
            internal string Metadata2 => Request.Purchase.Metadata2;

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
            [JsonProperty("description")]
            internal string Description { get; set; }
        }
    }

}
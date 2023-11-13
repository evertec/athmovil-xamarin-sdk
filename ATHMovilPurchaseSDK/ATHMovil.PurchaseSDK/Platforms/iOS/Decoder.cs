using System;
using System.Collections.Generic;
using ATHMovil.Purchase.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace ATHMovil.Purchase.Decoders
{
    public class Decoder : IDecodable
    {
        PurchaseResponse IDecodable.Decode(string responseJSON)
        {

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    args.ErrorContext.Handled = true;
                }
            };

            Response response = JsonConvert.DeserializeObject<Response>(responseJSON, jsonSettings);

            if (response == null)
            {
                return null;
            }

            List<PurchaseItem> listItems = new List<PurchaseItem>();

            if (response.Items != null && response.Items.Count() > 0) {
                foreach (var item in response.Items)
                {
                    PurchaseItem newItem = new PurchaseItem(item.Name, item.Quantity, item.Price)
                    {
                        Description = item.Description,
                        Metadata = item.Metadata
                    };
                    listItems.Add(newItem);
                }
            }
            

            PurchaseUser user = new PurchaseUser(response.Name, response.Telephone, response.Email);
            PurchaseInfo info = new PurchaseInfo(response.Status, response.DailyTransactionID, response.TransactionDate, response.ReferenceNumber);
            Model.Purchase purchase = new Model.Purchase(response.Total)
            {
                SubTotal = response.SubTotal,
                Tax = response.Tax,
                Fee = response.Fee,
                NetAmount = response.NetAmount,
                Metadata1 = response.Metadata1,
                Metadata2 = response.Metadata2,
                Items = listItems
            };

            return new PurchaseResponse(purchase, user, info);
        }

        private class Response
        {
            [JsonProperty("name")]
            public string Name { get; private  set; }

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
            public string StatusiOS { get; private set; }

            internal PurchaseState Status
            {
                get
                {
                    if (StatusiOS.ToUpper().Contains("COMPLETED"))
                    {
                        return PurchaseState.completed;
                    }else if (StatusiOS.ToUpper().Contains("SUCCESS"))
                    {
                        return PurchaseState.completed;
                    }
                    else if (StatusiOS.ToUpper().Contains("EXPIRED"))
                    {
                        return PurchaseState.expired;
                    }
                    else if (StatusiOS.ToUpper().Contains("TIMEOUT"))
                    {
                        return PurchaseState.expired;
                    }
                    else if (StatusiOS.ToUpper().Contains("FAILED"))
                    {
                        return PurchaseState.failed;
                    }
                    else
                    {
                        return PurchaseState.cancelled;
                    }
                }
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
            [JsonProperty("desc")]
            internal string Description { get; set; }
        }
    }
}

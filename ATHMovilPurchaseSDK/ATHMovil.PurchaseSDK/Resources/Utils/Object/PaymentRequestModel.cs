using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ATHMovil.Purchase.Utils
{
    [Serializable]
    public class PaymentRequestModel
    {
        [JsonProperty("env")]
        public string Env { get; set; }

        [JsonProperty("publicToken")]
        public string PubToken { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("tax")]
        public double Tax { get; set; }

        [JsonProperty("subtotal")]
        public double Subtotal { get; set; }

        [JsonProperty("metadata1")]
        public string Metadata1 { get; set; }

        [JsonProperty("metadata2")]
        public string Metadata2 { get; set; }

        [JsonProperty("items")]
        public List<Items> Items { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("ecommerceId")]
        public string EcommerceId { get; set; }


        public void PaymentRequest(string env, string publicToken, long timeout, double total, double tax, double subtotal, string metadata1, string metadata2, List<Items> items, string phoneNumber)
        {
            Env = env;
            PubToken = publicToken;
            Timeout = timeout;
            Total = total;
            Tax = tax;
            Subtotal = subtotal;
            Metadata1 = metadata1;
            Metadata2 = metadata2;
            Items = items;
            PhoneNumber = phoneNumber;
        }
    }
}


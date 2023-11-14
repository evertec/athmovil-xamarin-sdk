using System;
using Newtonsoft.Json;

namespace ATHMovil.Purchase.Utils
{
	public class AutorizationObject
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

        [JsonProperty("ecommerceStatus")]
        public string EcommerceStatus { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("dailyTransactionId")]
        public string DailyTransactionId { get; set; }

        [JsonProperty("totalRefundedAmount")]
        public string TotalRefundedAmount { get; set; }

        [JsonProperty("netAmount")]
        public double NetAmount { get; set; }

        [JsonProperty("fee")]
        public double Fee { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public AutorizationObject()
        {
        }

        public AutorizationObject(string env, string pubToken, long timeout, double total, double tax, double subtotal, string metadata1, string metadata2, List<Items> items, string phoneNumber)
        {
            Env = env;
            PubToken = pubToken;
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


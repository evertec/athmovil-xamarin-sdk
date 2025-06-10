using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATHMovil.Purchase.Extension;
using ATHMovil.Purchase.Storage;
using Newtonsoft.Json;
using ATHMovil.PurchaseSDK.String;

namespace ATHMovil.Purchase.Model
{
    public sealed class Purchase : ISatisfiable
    {
        private double _total { get; set; }
        private double _subTotal { get; set; }
        private double _tax { get; set; }
        private string _metadata1 { get; set; }
        private string _metadata2 { get; set; }
        private string _phoneNumber { get; set; }
        private string _ecommerceId { get; set; }
        private string _publictoken { get; set; }
        private int _timeout { get; set; }
        private string _env { get; set; }
        private List<PurchaseItem> _items { get; set; }

        [JsonProperty("total")]
        public double Total
        {
            get => _total;
            internal set
            {
                _total = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        [JsonProperty("subtotal")]
        public double SubTotal {
            get => _subTotal;
            set
            {
                _subTotal = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        [JsonProperty("tax")]
        public double Tax {
            get => _tax;
            set
            {
                _tax = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        [JsonIgnore] //[JsonProperty("fee")]
        public double Fee { get; set; }

        [JsonIgnore] //[JsonProperty("netAmount")]
        public double NetAmount { get; set; }

        [JsonProperty("items")]
        public List<PurchaseItem> Items {
            get => _items;
            set
            {
                if (value == null)
                {
                    _items = new List<PurchaseItem>();
                    return;
                }
                _items = value;
            }
        }


        [JsonProperty("metadata1")]
        public string Metadata1 {
            get => _metadata1;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _metadata1 = string.Empty;
                    return;
                }
                _metadata1 = value.Trim();
            }
        }

        [JsonProperty("metadata2")]
        public string Metadata2 {
            get => _metadata2;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _metadata2 = string.Empty;
                    return;
                }
                _metadata2 = value.Trim();
            }
        }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _phoneNumber = string.Empty;
                    return;
                }
                _phoneNumber = value.Trim();
                return;
            }
        }

        [JsonProperty("ecommerceId")]
        public string EcommerceId
        {
            get => _ecommerceId;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ecommerceId = string.Empty;
                    return;
                }
                _ecommerceId = value.Trim();
                return;
            }
        }

        [JsonProperty("publicToken")]
        public string PublicToken
        {
            get => _publictoken;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _publictoken = string.Empty;
                    return;
                }
                _publictoken = value.Trim();
                return;
            }
        }

        [JsonProperty("timeout")]
        public int TimeOut
        {
            get => _timeout;
            set
            {
                if (value == null || value == 0)
                {
                    _timeout = 600;
                    return;
                }
                _timeout = value;
                return;
            }
        }

        [JsonProperty("env")]
        public string Enviroment
        {
            get => _env;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _env = string.Empty;
                    return;
                }
                _env = value.Trim();
                return;
            }
        }

        [JsonIgnore]
        private PurchaseException? IsItemsSatisfy
        {
            get
            {
                IEnumerable<PurchaseException?> itemsError = from item in Items where item.IsSatisfy != null select item.IsSatisfy;
                return itemsError.FirstOrDefault();
            }
        }

        [JsonIgnore]
        public PurchaseException? IsSatisfy
        { 
            get
            {
                
                if (Total.IsLessThanMinimum())
                {
                    return PurchaseException.Request(getMessageError("Total data type value is invalid"));

                }

                if (SubTotal.IsNanOrNegative())
                {
                    return PurchaseException.Request(getMessageError("Subtotal data type value is invalid"));

                }

                if (Tax.IsNanOrNegative())
                {
                   return PurchaseException.Request(getMessageError("Tax data type value is invalid"));

                }

                if (Metadata1.Length > 40)
                {
                    return PurchaseException.Request(getMessageError("Metadata1 can not be greater than 40 characters"));

                }

                if (Metadata2.Length > 40)
                {
                    return PurchaseException.Request(getMessageError("Metadata2 can not be greater than 40 characters"));
                }

                PurchaseException? errorItems = IsItemsSatisfy;
                if (errorItems != null)
                {
                    return errorItems;
                }

                return null;
            }
        }

        public string  getMessageError(string message) {

            string genericMensagge = StringMensaje.GetGenericErrorMessage();

            return genericMensagge;
        }
        
        public Purchase(double total)
        {
            Total = total;
            SubTotal = 0;
            Tax = 0;
            Items = new List<PurchaseItem>();
            Metadata1 = string.Empty;
            Metadata2 = string.Empty;
            Fee = 0;
            NetAmount = 0;
        }
        public Purchase() {
            Total = 1;
            SubTotal = 0;
            Tax = 0;
            Items = new List<PurchaseItem>();
            Metadata1 = string.Empty;
            Metadata2 = string.Empty;
            Fee = 0;
            NetAmount = 0;
        }

    }
}

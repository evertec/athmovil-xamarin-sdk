using System;
using System.Collections.Generic;
using System.Linq;
using ATHMovil.Purchase.Extension;

namespace ATHMovil.Purchase.Model
{
    public sealed class Purchase : ISatisfiable
    {
        private double _total { get; set; }
        private double _subTotal { get; set; }
        private double _tax { get; set; }
        private string _metadata1 { get; set; }
        private string _metadata2 { get; set; }
        private List<PurchaseItem> _items { get; set; }

        public double Total
        {
            get => _total;
            internal set
            {
                _total = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        public double SubTotal {
            get => _subTotal;
            set
            {
                _subTotal = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        public double Tax {
            get => _tax;
            set
            {
                _tax = Math.Round(Math.Min(value, double.MaxValue), 2);
            }
        }

        public double Fee { get; internal set; }
        public double NetAmount { get; internal set; }

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

        private PurchaseException? IsItemsSatisfy
        {
            get
            {
                IEnumerable<PurchaseException?> itemsError = from item in Items where item.IsSatisfy != null select item.IsSatisfy;
                return itemsError.FirstOrDefault();
            }
        }

        public PurchaseException? IsSatisfy
        {
            get
            {
                if (Total.IsLessThanMinimum())
                {
                    return PurchaseException.Request("Total data type value is invalid");
                }

                if (SubTotal.IsNanOrNegative())
                {
                    return PurchaseException.Request("Subtotal data type value is invalid");
                }

                if (Tax.IsNanOrNegative())
                {
                    return PurchaseException.Request("Tax data type value is invalid");
                }

                if (Metadata1.Length > 40)
                {
                    return PurchaseException.Request("Metadata1 can not be greater than 40 characters");
                }

                if (Metadata2.Length > 40)
                {
                    return PurchaseException.Request("Metadata2 can not be greater than 40 characters");
                }

                PurchaseException? errorItems = IsItemsSatisfy;
                if (errorItems != null)
                {
                    return errorItems;
                }

                return null;
            }
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

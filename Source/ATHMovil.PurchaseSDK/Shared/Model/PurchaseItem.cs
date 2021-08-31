using System;
using ATHMovil.Purchase.Extension;

namespace ATHMovil.Purchase.Model
{
    public struct PurchaseItem : ISatisfiable
    {
        private string _name { get; set; }
        private double _price { get; set; }
        private int _quantity { get; set; }
        private string _description { get; set; }
        private string _metadata { get; set; }

        public string Name {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _name = string.Empty;
                    return;
                }
                _name = value.Trim();
            }
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                _quantity = Math.Min(value, int.MaxValue);
            }
        }

        public double Price
        {
            get => _price;
            private set
            {
                _price = Math.Round(Math.Min(value, double.MaxValue), 2); 
            }
        }


        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _description = string.Empty;
                    return;
                }
                _description = value.Trim();
            }
        }

        public string Metadata
        {
            get => _metadata;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _metadata = string.Empty;
                    return;
                }
                _metadata = value.Trim();
            }
        }

        public PurchaseException? IsSatisfy
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return PurchaseException.Request("Item's name value is required");
                }

                if (Quantity.IsLessThanMinimum())
                {
                    return PurchaseException.Request("Item's quantity data type value is invalid");
                }

                if (Price.IsLessThanMinimum())
                {
                    return PurchaseException.Request("Item's price data type value is invalid");
                }

                Name = Name.Trim();
                Description = Description.Trim();
                Metadata = Metadata.Trim();

                return null;
            }
        }

        public PurchaseItem(
            string name,
            int quantity,
            double price)
        {
            _name = string.Empty;
            _quantity = 0;
            _price = 1;
            _description = string.Empty;
            _metadata = string.Empty;
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = string.Empty;
            Metadata = string.Empty;

        }
    }
}

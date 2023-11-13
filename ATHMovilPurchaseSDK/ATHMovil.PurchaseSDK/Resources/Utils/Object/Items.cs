using System;
namespace ATHMovil.Purchase.Utils
{
    [Serializable]
    public class Items
    {
        private string name;
        private string description;
        private double price;
        private long quantity;
        private string metadata;

        public Items(string name, string desc, double price, long quantity, string metadata)
        {
            this.name = name;
            this.description = desc;
            this.price = price;
            this.quantity = quantity;
            this.metadata = metadata;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Desc
        {
            get { return description; }
            set { description = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public long Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }
    }
}


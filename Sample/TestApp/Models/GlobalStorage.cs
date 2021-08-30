using System;
using System.Collections.Generic;
using System.Threading;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace TestApp.Models
{
    public class GlobalStorage
    {
        internal static Lazy<GlobalStorage> ShareInstance = new Lazy<GlobalStorage>(() => new GlobalStorage(), LazyThreadSafetyMode.PublicationOnly);
        
        private GlobalStorage()
        {
        }

        public string SelectedTheme
        {
            get => Preferences.Get("Theme", ATHMovil.Purchase.UI.Theme.Classic.ToString());
            set => Preferences.Set("Theme", value);
        }

        public string SelectedEnviroment
        {
            get => Preferences.Get("Enviroment", ATHMovilTarget.Production.ToString());
            set => Preferences.Set("Enviroment", value);
        }

        public string Token
        {
            get => Preferences.Get("Token", "Dummy");
            set => Preferences.Set("Token", value);
        }

        public int TimeOut
        {
            get => Preferences.Get("Timeout", 600);
            set => Preferences.Set("Timeout", value);
        }

        public double Total
        {
            get => Preferences.Get("Total", 1.0);
            set => Preferences.Set("Total", value);
        }

        public double SubTotal
        {
            get => Preferences.Get("SubTotal", 0.0);
            set => Preferences.Set("SubTotal", value);
        }

        public double Tax
        {
            get => Preferences.Get("Tax", 0.0);
            set => Preferences.Set("Tax", value);
        }

        public string Metadata1
        {
            get => Preferences.Get("Metadata1", string.Empty);
            set => Preferences.Set("Metadata1", value);
        }

        public string Metadata2
        {
            get => Preferences.Get("Metadata2", string.Empty);
            set => Preferences.Set("Metadata2", value);
        }

        public List<PurchaseItem> Items {
            get {
                string savedItems = Preferences.Get("Items", string.Empty);

                if (string.IsNullOrEmpty(savedItems))
                {
                    return new List<PurchaseItem>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<PurchaseItem>>(savedItems);
                }
            }
            set
            {
                string itemsToSave = JsonConvert.SerializeObject(value);
                Preferences.Set("Items", itemsToSave);
            }
        }
    }
}

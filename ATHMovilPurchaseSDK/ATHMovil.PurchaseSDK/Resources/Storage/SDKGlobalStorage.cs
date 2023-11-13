using System;
using System.Collections.Generic;
using System.Threading;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using Newtonsoft.Json;
using Microsoft.Maui;

namespace ATHMovil.Purchase.Storage
{
    public class SDKGlobalStorage
    {
        internal static Lazy<SDKGlobalStorage> ShareInstance = new Lazy<SDKGlobalStorage>(() => new SDKGlobalStorage(), LazyThreadSafetyMode.PublicationOnly);

        private SDKGlobalStorage()
        {
        }

        public string EcommerceID
        {
            get => Preferences.Get("EcommerceID", "");
            set => Preferences.Set("EcommerceID", value);
        }

        public string Token
        {
            get => Preferences.Get("Token", "");
            set => Preferences.Set("Token", value);
        }

        public string PublicToken
        {
            get => Preferences.Get("PublicToken", "");
            set => Preferences.Set("PublicToken", value);
        }

        public string Flow
        {
            get => Preferences.Get("Flow", "");
            set => Preferences.Set("Flow", value);
        }
    }
}


using System;
using System.Collections.Generic;

namespace ATHMovil.Purchase.Storage
{
    public class SDKGlobal
    {
        public String EcommerceID { get; set; }
        public String Token { get; set; }
        public String PublicToken { get; set; }

        private static SDKGlobal _instance = null;

        private SDKGlobal()
        {
            
        }

        static public SDKGlobal Instance()
        {
            if (_instance == null)
            {
                _instance = new SDKGlobal();
                _instance.EcommerceID = SDKGlobalStorage.ShareInstance.Value.EcommerceID;
                _instance.Token = SDKGlobalStorage.ShareInstance.Value.Token;
                _instance.PublicToken = SDKGlobalStorage.ShareInstance.Value.PublicToken;

                return _instance;
            }

            return _instance;
        }
    }
}


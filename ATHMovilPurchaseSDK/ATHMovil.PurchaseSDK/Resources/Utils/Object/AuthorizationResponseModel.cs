using System;
namespace ATHMovil.Purchase.Utils
{
    [Serializable]
    public class AuthorizationResponse
    {
        public string Status { get; set; }
        public AutorizationObject Data { get; set; }
    }
}


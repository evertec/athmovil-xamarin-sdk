using System;
namespace ATHMovil.Purchase.Model
{
    public struct Business : ISatisfiable
    {
        private string _publicToken { get; set; }
        public string PublicToken
        {
            get => _publicToken;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _publicToken = string.Empty;
                    return;
                }
                _publicToken = value.Trim();
            }
        }

        internal bool IsDummy
        {
            get
            {
                if (PublicToken != null && !string.IsNullOrEmpty(PublicToken) && string.Compare(PublicToken, "dummy", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public PurchaseException? IsSatisfy
        {
            get {
                
                if (string.IsNullOrEmpty(PublicToken))
                {
                    return PurchaseException.Request("The business's token is required");
                }
                return null;
            }
        }

        public Business(string publicToken)
        {
            _publicToken = string.Empty;
            PublicToken = publicToken;
        }
    }
}

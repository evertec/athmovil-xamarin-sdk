namespace ATHMovil.Purchase.Model.Config
{
    public enum ATHMovilTarget
    {
        Production
    }

    public struct UriCallBack : ISatisfiable
    {
        public string _scheme { get; private set; }
        public string Scheme
        {
            get => _scheme;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _scheme = string.Empty;
                    return;
                }
                _scheme = value.Trim();
            }
        }

        public PurchaseException? IsSatisfy
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Scheme))
                {
                    return PurchaseException.Request("The url scheme is required");
                }

                Scheme = Scheme.Trim();

                return null;
            }
        }

        public UriCallBack(string scheme)
        {
            _scheme = string.Empty;
            Scheme = scheme;
        }
    }
}

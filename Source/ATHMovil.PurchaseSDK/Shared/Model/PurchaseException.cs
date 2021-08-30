namespace ATHMovil.Purchase.Model
{
    public struct PurchaseException
    {
        public string FailureReason { get; set; }
        public string Description { get; set; }
        public bool IsRequestError { get; set; }

        internal static PurchaseException Request(string description)
        {
            return new PurchaseException()
            {
                FailureReason = "Error in Request",
                Description = description,
                IsRequestError = true
            };
        }

        internal static PurchaseException Response(string description)
        {
            return new PurchaseException()
            {
                FailureReason = "Error in Response",
                Description = description,
                IsRequestError = false
            };
        }
    }
}

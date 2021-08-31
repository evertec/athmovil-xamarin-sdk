using System;

namespace ATHMovil.Purchase.Model
{

    public enum PurchaseState
    {
        cancelled,
        completed,
        expired,
    }

    public sealed class PurchaseInfo
    {
        public int DailyTransactionID { get; private set; }
        public string ReferenceNumber { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public PurchaseState Status { get; private set; }

        private PurchaseInfo()
        {
        }

        internal PurchaseInfo(PurchaseState status, int daily, DateTime date, string referenceNumber)
        {
            Status = status;
            DailyTransactionID = daily;
            TransactionDate = date;
            ReferenceNumber = referenceNumber;
        }
    }
}

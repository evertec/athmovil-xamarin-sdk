using System;

namespace ATHMovil.Purchase.Model
{

    public enum PurchaseState
    {
        cancelled,
        completed,
        expired,
        failed,
    }

    public sealed class PurchaseInfo
    {
        public int DailyTransactionID { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; private set; }
        public PurchaseState Status { get; set; }

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

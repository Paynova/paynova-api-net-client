namespace Paynova.Api.Client.Responses
{
    public class FinalizeAuthorizationResponse : Response
    {
        public string TransactionId { get; set; }
        public string BatchId { get; set; }
        public string AcquirerId { get; set; }
        public decimal TotalAmountFinalized { get; set; }
        public decimal TotalAmountPendingFinalization { get; set; }
        public bool CanFinalizeAgain { get; set; }
        public decimal AmountRemainingForFinalization { get; set; }
    }
}
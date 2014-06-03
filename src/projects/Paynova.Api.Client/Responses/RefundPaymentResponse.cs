namespace Paynova.Api.Client.Responses
{
    public class RefundPaymentResponse : Response
    {
        public string TransactionId { get; set; }
        public string BatchId { get; set; }
        public string AcquirerId { get; set; }
        public decimal TotalAmountRefunded { get; set; }
        public decimal TotalAmountPendingRefund { get; set; }
        public bool CanRefundAgain { get; set; }
        public decimal AmountRemainingForRefund { get; set; }
    }
}
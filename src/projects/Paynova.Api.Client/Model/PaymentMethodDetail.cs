namespace Paynova.Api.Client.Model
{
    public class PaymentMethodDetail
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodProductId { get; set; }
        public string DisplayName { get; set; }
        public KeyedDisplayName Group { get; set; }
        public LabelSymbolValue<decimal> InterestRate { get; set; }
        public LabelSymbolValue<decimal> NotificationFee { get; set; }
        public LabelSymbolValue<decimal> SetupFee { get; set; }
        public decimal? NumberOfInstallments { get; set; }
        public int? InstallmentPeriod { get; set; }
        public string InstallmentUnit { get; set; }
        public Link[] LegalDocuments { get; set; }
        public string[] AddressTypeRestrictions { get; set; }

        public PaymentMethodDetail()
        {
            AddressTypeRestrictions = new string[0];
            LegalDocuments = new Link[0];
        }
    }
}